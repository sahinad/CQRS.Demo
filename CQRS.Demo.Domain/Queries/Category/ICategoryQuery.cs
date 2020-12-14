using System.Data.Entity.Infrastructure;

namespace CQRS.Demo.Domain.Queries.Category
{
    public interface ICategoryQuery : IQueryObject<Models.Category>
    {
    }
}
