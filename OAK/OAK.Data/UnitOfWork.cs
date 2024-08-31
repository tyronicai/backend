using Microsoft.EntityFrameworkCore.Storage;

namespace OAK.Data
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Data.Common;

    public class UnitOfWork<TContext> : IRepositoryFactory, IUnitOfWork<TContext>, IUnitOfWork
        where TContext : DbContext, IDisposable
    {
        //private Dictionary<Type, object> _repositories;
        private Dictionary<Type, object> _readonlyRepositories;
        private Dictionary<Type, object> _asyncRepositories;
        private Dictionary<Type, object> _repositories;


        public UnitOfWork(TContext context)
        {
            DbConnection conn = context.Database.GetDbConnection();
            var conn2 = conn;
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type)) _repositories[type] = new Repository<TEntity>(Context);
            return (IRepository<TEntity>)_repositories[type];
        }

        public IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class
        {
            if (_asyncRepositories == null) _asyncRepositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!_asyncRepositories.ContainsKey(type)) _asyncRepositories[type] = new RepositoryAsync<TEntity>(Context);
            return (IRepositoryAsync<TEntity>)_asyncRepositories[type];
        }

        public IRepositoryReadOnly<TEntity> GetReadOnlyRepository<TEntity>() where TEntity : class
        {
            if (_readonlyRepositories == null) _readonlyRepositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!_readonlyRepositories.ContainsKey(type)) _readonlyRepositories[type] = new RepositoryReadOnly<TEntity>(Context);
            return (IRepositoryReadOnly<TEntity>)_readonlyRepositories[type];
        }

        public TContext Context { get; }

        public int SaveChanges()
        {
            try
            {
                return Context.SaveChanges();
            }
            catch (Exception ex)
            {
                string ex2 = ex.Message;
                return 0;
            }
        }

        public IDbContextTransaction BeginTransaction(IDbContextTransaction trans = null)
        {
            if (null == trans)
            {
                trans = Context.Database.BeginTransaction();
            }
            else if (null == Context.Database.CurrentTransaction)
            {
                Context.Database.UseTransaction(trans.GetDbTransaction());
            }
            return trans;
        }

        public void UseTransaction(IDbContextTransaction trans = null)
        {
            if (null != trans)
            {
                var dbTrans = trans.GetDbTransaction();
                Context.Database.UseTransaction(dbTrans);
            }
        }
        public void CommitTransaction(IDbContextTransaction trans = null)
        {
            if (null == trans)
            {
                Context.Database.CommitTransaction();
            }
            else
            {
                trans.Commit();
            }
        }
        public void RollbackTransaction(IDbContextTransaction trans = null)
        {
            if (null == trans)
            {
                Context.Database.RollbackTransaction();
            }
            else
            {
                trans.Rollback();
            }
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}