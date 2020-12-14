using CQRS.Demo.Domain.Commands;
using CQRS.Demo.Domain.Commands.Product;
using CQRS.Demo.DomainServices.QueryServices.Product;
using CQRS.Demo.DomainServices.Validators;
using System;
using System.Threading.Tasks;

namespace CQRS.Demo.DomainServices.CommandServices.Product
{
    public class ProductCommandService : IProductCommandService
    {
        private readonly ICommandFactory _commandFactory;
        private readonly IProductQueryService _productQueryService;
        private readonly IProductCommand _productCommand;

        public ProductCommandService(ICommandFactory commandFactory, IProductQueryService productQueryService)
        {
            _commandFactory = commandFactory ?? throw new ArgumentNullException(nameof(commandFactory));
            _productQueryService = productQueryService ?? throw new ArgumentNullException(nameof(productQueryService));
            _productCommand = _commandFactory.CreateCommand<IProductCommand>();
        }

        public async Task AddOrUpdateProductAsync(Domain.Models.Product product)
        {
            var validator = new ProductValidator();
            await validator.ValidateAsync(product);

            await _productCommand.AddOrUpdateAsync(product);
        }

        public async Task DeleteProductAsync(string productName)
        {
            var product = await _productQueryService.GetProductByName(productName);

            await _productCommand.DeleteAsync(product);
        }
    }
}
