using System.Threading.Tasks;

namespace CQRS.Demo.DomainServices.CommandServices.Category
{
    public interface ICategoryCommandService
    {
        Task AddOrUpdateCategoryAsync(Domain.Models.Category category);

        Task DeleteCategoryAsync(string categoryName);
    }
}
