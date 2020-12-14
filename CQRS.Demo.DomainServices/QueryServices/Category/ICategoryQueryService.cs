using System.Collections.Generic;
using System.Threading.Tasks;

namespace CQRS.Demo.DomainServices.QueryServices.Category
{
    public interface ICategoryQueryService
    {
        Task<List<Domain.Models.Category>> GetCategoriesAsync();

        Task<Domain.Models.Category> GetCategoryByNameAsync(string categoryName);

        Task<int> CheckIfCategoryExists(string categoryName);
    }
}
