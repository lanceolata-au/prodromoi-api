using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Prodromoi.Core.Dtos;
using Prodromoi.Core.Interfaces;
using Serilog;

namespace Prodromoi.Core.Features
{
    public sealed class AuditedRepository : IAuditRepository
    {
        
        private readonly DbContext _context;    

        public AuditedRepository(DbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            try
            {
                var result = _context.SaveChangesAsync().Result;
            }
            catch (Exception e)
            {
                e.LogException();
            }
        }

        private IQueryable<T> GetQueryable<T, TId>(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string? includeProperties = null,
            int? skip = null,
            int? take = null)
            where T : AuditEntity<TId> where TId : struct
        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<T> query = _context.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        public IQueryable<T> Table<T, TId>(
            Func<IQueryable<T>, 
            IOrderedQueryable<T>>? orderBy = null, 
            string? includeProperties = null, 
            int? skip = null, int? take = null) 
            where T : AuditEntity<TId> where TId : struct
        {
            return GetQueryable<T, TId>(null, orderBy, includeProperties, skip, take);
        }

        public T GetById<T, TId>(TId id) where T : AuditEntity<TId> where TId : struct
        {
            return _context.Set<T>().Find(id)!;
        }

        public Task<T> GetByIdAsync<T, TId>(TId id) where T : AuditEntity<TId> where TId : struct
        {
            return _context.Set<T>().FindAsync(id).AsTask()!;
        }

        public void Create<T, TId>(T entity) where T : AuditEntity<TId> where TId : struct
        {
            _context.Set<T>().Add(entity);
            AddAuditEntries(entity.AuditEntries);
        }

        public void Update<T, TId>(T entity) where T : AuditEntity<TId> where TId : struct
        {
            _context.Set<T>().Attach(entity);
            AddAuditEntries(entity.AuditEntries);
            _context.Entry(entity).State = EntityState.Modified;
        }

        private void AddAuditEntries(List<AuditEntry> updateEntries)
        {
            foreach (var entry in updateEntries)
            {
                _context
                    .Set<AuditEntry>()
                    .Add(entry);
            }
        }

        public void Delete<T, TId>(T entity) where T : AuditEntity<TId> where TId : struct
        {
            var dbSet = _context.Set<T>();
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            dbSet.Remove(entity);

        }

        public void Delete<T, TId>(TId id) where T : AuditEntity<TId> where TId : struct
        {
            var entity = _context.Set<T>().Find(id);
            
            Delete<T, TId>(entity!);
            
        }

        public void Commit()
        {
            try
            {
                var result = _context.SaveChangesAsync().Result;
            }
            catch (Exception e)
            {
                e.LogException();
            }
        }
    }
}