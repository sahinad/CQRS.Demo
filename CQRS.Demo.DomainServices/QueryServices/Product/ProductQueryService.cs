using CQRS.Demo.Domain.Queries;
using CQRS.Demo.Domain.Queries.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CQRS.Demo.DomainServices.QueryServices.Product
{
    public class ProductQueryService : IProductQueryService
    {
        private readonly IQueryFactory _queryFactory;

        public ProductQueryService(IQueryFactory queryFactory)
        {
            _queryFactory = queryFactory;
        }

        public async Task<int> CheckIfProductExists(string productName)
        {
            var product = await _queryFactory.CreateQuery<IProductQuery>()
                .FirstOrDefaultAsync(x => x.ProductName == productName);
            return product?.Id ?? 0;
        }

        public async Task<Domain.Models.Product> GetProductByName(string productName) =>
            await _queryFactory.CreateQuery<IProductQuery>()
                .FirstOrDefaultAsync(x => x.ProductName == productName);

        public async Task<List<Domain.Models.Product>> GetProductsByCategoryAsync() =>
            await _queryFactory.CreateQuery<IProductQuery>()
                .Include(x => x.Category)
                .Where(x => !x.Discontinued)
                .ToListAsync();
    }
}
