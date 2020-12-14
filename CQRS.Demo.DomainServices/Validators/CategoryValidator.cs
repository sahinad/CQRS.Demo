using CQRS.Demo.Domain.Models;
using FluentValidation;

namespace CQRS.Demo.DomainServices.Validators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.CategoryName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(15);
            RuleFor(x => x.Description)
                .NotNull()
                .MaximumLength(500);
        }
    }
}
