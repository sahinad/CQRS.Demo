using CQRS.Demo.Domain.Models;

namespace CQRS.Demo.Domain.Commands.Category
{
    public class CategoryCommand : CommandObject<Models.Category>, ICategoryCommand
    {
        public CategoryCommand(IDomainContextFactory domainContextFactory) : base(domainContextFactory)
        {

        }
    }
}
