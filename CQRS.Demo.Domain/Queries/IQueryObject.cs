using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CQRS.Demo.Domain.Queries
{
    public interface IQueryObject
    {
    }

    public interface IQueryObject<TEntity> : IQueryObject
        where TEntity : class
    {
        Task<TEntity> FirstOrDefaultAsync();
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression);
        Task<List<TEntity>> ToListAsync();
        IQueryObject<TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> navigationPropertyPath);
        IQueryObject<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> AnyAsync();
        IQueryObject<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IQueryObject<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IQueryObject<TEntity> Take(int count);

        Task<List<TEntity>> GroupByAndSelectToListAsync<TKey>(
            Expression<Func<TEntity, TKey>> keySelector,
            Expression<Func<IGrouping<TKey, TEntity>, TEntity>> expression
        );

        Task<List<TTarget>> SelectToListAsync<TTarget>(Expression<Func<TEntity, TTarget>> selector);
    }
}