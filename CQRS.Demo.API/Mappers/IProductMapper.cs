using CQRS.Demo.API.Models;
using CQRS.Demo.Domain.Models;
using System.Threading.Tasks;

namespace CQRS.Demo.API.Mappers
{
    public interface IProductMapper
    {
        Task<Product> MapToDomain(AddOrUpdateProduct addOrUpdateProduct);
    }
}
