using CQRS.Demo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CQRS.Demo.Domain.Queries
{
    public abstract class QueryObject<TEntity> : IQueryObject<TEntity>
        where TEntity : BaseModel
    {
        protected readonly IDomainReadonlyContext Context;

        protected QueryObject(IDomainReadonlyContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Query = Context.GetQueryable<TEntity>();
        }

        protected IQueryable<TEntity> Query { get; set; }

        public async Task<TEntity> FirstOrDefaultAsync()
        {
            return await Query.FirstOrDefaultAsync();
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Query.FirstOrDefaultAsync(expression);
        }

        public async Task<List<TEntity>> ToListAsync()
        {
            return await Query.ToListAsync();
        }

        public IQueryObject<TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> navigationPropertyPath)
        {
            Query = Query.Include(navigationPropertyPath);
            return this;
        }

        public IQueryObject<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            Query = Query.Where(predicate);
            return this;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Query.AnyAsync(predicate);
        }

        public async Task<bool> AnyAsync()
        {
            return await Query.AnyAsync();
        }

        public IQueryObject<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            Query = Query.OrderBy(keySelector);
            return this;
        }

        public IQueryObject<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            Query = Query.OrderByDescending(keySelector);
            return this;
        }

        public IQueryObject<TEntity> Take(int count)
        {
            Query = Query.Take(count);
            return this;
        }

        public async Task<List<TEntity>> GroupByAndSelectToListAsync<TKey>(
            Expression<Func<TEntity, TKey>> keySelector,
            Expression<Func<IGrouping<TKey, TEntity>, TEntity>> expression
        )
        {
            return await Query.GroupBy(keySelector).Select(expression).ToListAsync();
        }

        public async Task<List<TTarget>> SelectToListAsync<TTarget>(Expression<Func<TEntity, TTarget>> selector)
        {
            return await Query.Select(selector).ToListAsync();
        }
    }
}
