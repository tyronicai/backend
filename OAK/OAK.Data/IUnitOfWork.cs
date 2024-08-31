using Microsoft.EntityFrameworkCore.Storage;

namespace OAK.Data
{
    using Microsoft.EntityFrameworkCore;
    using System;

    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class;
        IRepositoryReadOnly<TEntity> GetReadOnlyRepository<TEntity>() where TEntity : class;

        int SaveChanges();
        IDbContextTransaction BeginTransaction(IDbContextTransaction trans = null);

        void UseTransaction(IDbContextTransaction trans = null);
        void CommitTransaction(IDbContextTransaction trans = null);

        void RollbackTransaction(IDbContextTransaction trans = null);
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext Context { get; }
    }

}