using CQRS.Demo.API.Models;
using CQRS.Demo.Domain.Models;
using CQRS.Demo.DomainServices.QueryServices.Category;
using CQRS.Demo.DomainServices.QueryServices.Product;
using CQRS.Demo.DomainServices.QueryServices.Supplier;
using FluentValidation;
using System;
using System.Threading.Tasks;

namespace CQRS.Demo.API.Mappers
{
    public class ProductMapper : IProductMapper
    {
        private readonly IProductQueryService _productQueryService;
        private readonly ICategoryQueryService _categoryQueryService;
        private readonly ISupplierQueryService _supplierQueryService;

        public ProductMapper(IProductQueryService productQueryService, ICategoryQueryService categoryQueryService, ISupplierQueryService supplierQueryService)
        {
            _productQueryService = productQueryService;
            _categoryQueryService = categoryQueryService;
            _supplierQueryService = supplierQueryService;
        }

        public async Task<Product> MapToDomain(AddOrUpdateProduct addOrUpdateProduct)
        {
            if (string.IsNullOrEmpty(addOrUpdateProduct.CategoryName) ||
                string.IsNullOrEmpty(addOrUpdateProduct.ProductName) ||
                string.IsNullOrEmpty(addOrUpdateProduct.SupplierCompanyName))
            {
                throw new ValidationException("CategoryName, ProductName and CompanyName are required.");
            }

            var categoryId = (await _categoryQueryService.GetCategoryByNameAsync(addOrUpdateProduct.CategoryName))?.Id ??
                throw new ArgumentException("No category has been found with the provided category name.");

            var supplierId = (await _supplierQueryService.GetSupplierByCompanyNameAsync(addOrUpdateProduct.SupplierCompanyName))?.Id ??
                throw new ArgumentException("No supplier has been found with the provided company name.");

            var productId = await _productQueryService.CheckIfProductExists(addOrUpdateProduct.ProductName);

            var product = new Product
            {
                CategoryId = categoryId,
                Discontinued = addOrUpdateProduct.Discontinued,
                Id = productId,
                ProductName = addOrUpdateProduct.ProductName,
                QuantityPerUnit = addOrUpdateProduct.QuantityPerUnit,
                ReorderLevel = addOrUpdateProduct.ReorderLevel,
                SupplierId = supplierId,
                UnitPrice = addOrUpdateProduct.UnitPrice,
                UnitsInStock = addOrUpdateProduct.UnitsInStock,
                UnitsOnOrder = addOrUpdateProduct.UnitsOnOrder
            };

            return product;
        }
    }
}
