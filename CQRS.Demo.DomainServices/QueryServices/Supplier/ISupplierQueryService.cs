using System.Threading.Tasks;

namespace CQRS.Demo.DomainServices.QueryServices.Supplier
{
    public interface ISupplierQueryService
    {
        Task<Domain.Models.Supplier> GetSupplierByCompanyNameAsync(string companyName);
    }
}
