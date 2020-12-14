using CQRS.Demo.API.Mappers;
using CQRS.Demo.API.Models;
using CQRS.Demo.DomainServices.CommandServices.Product;
using CQRS.Demo.DomainServices.QueryServices.Product;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS.Demo.API.Controllers
{
    [Route("v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ProductController : ControllerBase
    {
        private readonly IProductCommandService _productCommandService;
        private readonly IProductQueryService _productQueryService;
        private readonly IProductMapper _productMapper;

        public ProductController(
            IProductCommandService productCommandService,
            IProductQueryService productQueryService,
            IProductMapper productMapper)
        {
            _productCommandService = productCommandService;
            _productQueryService = productQueryService;
            _productMapper = productMapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateProducts(AddOrUpdateProduct addOrUpdateProduct)
        {
            if (addOrUpdateProduct is null)
            {
                throw new ArgumentNullException(nameof(addOrUpdateProduct));
            }

            try
            {
                var product = await _productMapper.MapToDomain(addOrUpdateProduct);

                await _productCommandService.AddOrUpdateProductAsync(product);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByCategory()
        {
            List<GetProductsByCategory> result;

            try
            {
                var products = await _productQueryService.GetProductsByCategoryAsync();

                result = products.Select(x =>
                    new GetProductsByCategory
                    {
                        CategoryName = x.Category.CategoryName,
                        Discontinued = x.Discontinued,
                        ProductName = x.ProductName,
                        QuantityPerUnit = x.QuantityPerUnit,
                        UnitsInStock = x.UnitsInStock
                    }).ToList();
            }
            catch (Exception)
            {
                throw;
            }


            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(string productName)
        {
            if (productName is null)
            {
                throw new ArgumentNullException(nameof(productName));
            }

            try
            {
                await _productCommandService.DeleteProductAsync(productName);
            }
            catch (Exception)
            {
                throw;
            }

            return Ok();
        }
    }
}
