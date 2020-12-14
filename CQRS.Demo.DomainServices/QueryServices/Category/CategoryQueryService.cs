using CQRS.Demo.Domain.Queries;
using CQRS.Demo.Domain.Queries.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CQRS.Demo.DomainServices.QueryServices.Category
{
    public class CategoryQueryService : ICategoryQueryService
    {
        private readonly IQueryFactory _queryFactory;

        public CategoryQueryService(IQueryFactory queryFactory)
        {
            _queryFactory = queryFactory;
        }

        public async Task<List<Domain.Models.Category>> GetCategoriesAsync() =>
            await _queryFactory.CreateQuery<ICategoryQuery>().ToListAsync();

        public async Task<int> CheckIfCategoryExists(string categoryName)
        {
            var category = await _queryFactory.CreateQuery<ICategoryQuery>().FirstOrDefaultAsync(x => x.CategoryName == categoryName);
            return category?.Id ?? 0;
        }

        public async Task<Domain.Models.Category> GetCategoryByNameAsync(string categoryName) =>
            await _queryFactory.CreateQuery<ICategoryQuery>().FirstOrDefaultAsync(x => x.CategoryName == categoryName);
    }
}
