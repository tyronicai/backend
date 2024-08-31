namespace OAK.Data
{
    using Microsoft.EntityFrameworkCore.Query;
    using OAK.Data.Paging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    public interface IReadRepository<T> where T : class
    {
        IQueryable<T> Query(string sql, params object[] parameters);

        T Search(params object[] keyValues);

        T Single(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true);

        IPaginate<T> GetList(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int index = 0,
            int size = 20,
            bool disableTracking = true);

        IPaginate<TResult> GetList<TResult>(Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int index = 0,
            int size = 20,
            bool disableTracking = true) where TResult : class;

        List<T> GetAllReadOnly();

        List<T> GetAllReadOnly(Expression<Func<T, bool>> predicate);

        List<T> GetAllReadOnly(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        IQueryable<T> GetQueryable(Expression<Func<T, bool>> predicate = null,
                                                         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                         Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                                         bool disableTracking = true);

    }
}