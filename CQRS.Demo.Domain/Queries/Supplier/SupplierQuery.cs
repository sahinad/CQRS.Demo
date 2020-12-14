using CQRS.Demo.Domain.Models;

namespace CQRS.Demo.Domain.Queries.Supplier
{
    public class SupplierQuery : QueryObject<Models.Supplier>, ISupplierQuery
    {
        public SupplierQuery(DomainReadonlyContext readonlyContext) : base(readonlyContext)
        {

        }
    }
}
