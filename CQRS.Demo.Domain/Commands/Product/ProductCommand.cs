using CQRS.Demo.Domain.Models;

namespace CQRS.Demo.Domain.Commands.Product
{
    public class ProductCommand : CommandObject<Models.Product>, IProductCommand
    {
        public ProductCommand(IDomainContextFactory domainContextFactory) : base(domainContextFactory)
        {

        }
    }
}
