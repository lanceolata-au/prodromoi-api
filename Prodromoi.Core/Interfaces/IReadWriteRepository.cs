﻿using Prodromoi.Core.Features;

namespace Prodromoi.Core.Interfaces
{
    public interface IReadWriteRepository : IReadOnlyRepository
    {
        void Create<T, TId>(T entity) where T : Entity<TId> where TId : struct;
        
        void Update<T, TId>(T entity) where T : Entity<TId> where TId : struct;
        
        void Delete<T, TId>(T entity) where T : Entity<TId> where TId : struct;
        
        void Delete<T, TId>(TId id) where T : Entity<TId> where TId : struct;

        void Commit();
    }
}