using System.Threading.Tasks;

namespace CQRS.Demo.DomainServices.CommandServices.Product
{
    public interface IProductCommandService
    {
        Task AddOrUpdateProductAsync(Domain.Models.Product product);

        Task DeleteProductAsync(string productName);
    }
}
