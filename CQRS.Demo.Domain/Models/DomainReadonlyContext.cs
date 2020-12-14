using System;
using System.Data.Entity;
using System.Linq;

namespace CQRS.Demo.Domain.Models
{
    public class DomainReadonlyContext : IDomainReadonlyContext, IDisposable
    {
        private readonly DomainContext _context;

        public DomainReadonlyContext(IDomainContextFactory contextFactory)
        {
            _context = contextFactory.CreateDbContext();
        }

        IQueryable<TEntity> IDomainReadonlyContext.GetQueryable<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>().AsNoTracking();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
