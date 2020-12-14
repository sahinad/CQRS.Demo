using CQRS.Demo.Domain.Queries;
using CQRS.Demo.Domain.Queries.Supplier;
using System.Threading.Tasks;

namespace CQRS.Demo.DomainServices.QueryServices.Supplier
{
    public class SupplierQueryService : ISupplierQueryService
    {
        private readonly IQueryFactory _queryFactory;

        public SupplierQueryService(IQueryFactory queryFactory)
        {
            _queryFactory = queryFactory;
        }

        public async Task<Domain.Models.Supplier> GetSupplierByCompanyNameAsync(string companyName) =>
            await _queryFactory.CreateQuery<ISupplierQuery>()
                .FirstOrDefaultAsync(x => x.CompanyName == companyName);
    }
}
