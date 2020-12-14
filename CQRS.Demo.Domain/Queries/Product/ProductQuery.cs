using CQRS.Demo.Domain.Models;

namespace CQRS.Demo.Domain.Queries.Product
{
    public class ProductQuery : QueryObject<Models.Product>, IProductQuery
    {
        public ProductQuery(DomainReadonlyContext readonlyContext) : base(readonlyContext)
        {

        }
    }
}
