using CQRS.Demo.Domain.Models;

namespace CQRS.Demo.Domain.Queries.Category
{
    public class CategoryQuery : QueryObject<Models.Category>, ICategoryQuery
    {
        public CategoryQuery(DomainReadonlyContext readonlyContext) : base(readonlyContext)
        {

        }
    }
}
