using Prodromoi.Core.Features;

namespace Prodromoi.Core.Interfaces;

public interface IAuditRepository : IDisposable
{
    IQueryable<T> Table<T, TId>(
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string? includeProperties = null,
        int? skip = null,
        int? take = null)
        where T : AuditEntity<TId> where TId : struct;
        
    T GetById<T, TId>(TId id) where T : AuditEntity<TId> where TId : struct;
        
    Task<T> GetByIdAsync<T, TId>(TId id) where T : AuditEntity<TId> where TId : struct;
    
    void Create<T, TId>(T entity) where T : AuditEntity<TId> where TId : struct;
        
    void Update<T, TId>(T entity) where T : AuditEntity<TId> where TId : struct;
        
    void Delete<T, TId>(T entity) where T : AuditEntity<TId> where TId : struct;
        
    void Delete<T, TId>(TId id) where T : AuditEntity<TId> where TId : struct;

    void Commit();
    
}