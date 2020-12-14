using CQRS.Demo.Domain.Models;
using FluentValidation;

namespace CQRS.Demo.DomainServices.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.ProductName).MaximumLength(40);
            RuleFor(x => x.QuantityPerUnit).MaximumLength(20);
            RuleFor(x => x.ProductName).MaximumLength(40);
            RuleFor(x => x.UnitPrice).ScalePrecision(2, 6);
        }
    }
}
