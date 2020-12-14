using System.Collections.Generic;
using System.Threading.Tasks;

namespace CQRS.Demo.DomainServices.QueryServices.Product
{
    public interface IProductQueryService
    {
        Task<List<Domain.Models.Product>> GetProductsByCategoryAsync();

        Task<Domain.Models.Product> GetProductByName(string productName);

        Task<int> CheckIfProductExists(string productName);
    }
}
