using System.Linq;

namespace CQRS.Demo.Domain.Models
{
    public interface IDomainReadonlyContext
    {
        IQueryable<TEntity> GetQueryable<TEntity>() where TEntity : class;
    }
}
