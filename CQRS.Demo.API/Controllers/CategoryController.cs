using CQRS.Demo.API.Models;
using CQRS.Demo.Domain.Models;
using CQRS.Demo.DomainServices.CommandServices.Category;
using CQRS.Demo.DomainServices.QueryServices.Category;
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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryCommandService _categoryCommandService;
        private readonly ICategoryQueryService _categoryQueryService;

        public CategoryController(
            ICategoryCommandService categoryCommandService,
            ICategoryQueryService categoryQueryService)
        {
            _categoryCommandService = categoryCommandService;
            _categoryQueryService = categoryQueryService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCategory(AddOrUpdateCategory addOrUpdateCategory)
        {
            if (addOrUpdateCategory is null)
            {
                throw new ArgumentNullException(nameof(addOrUpdateCategory));
            }

            var category = new Category
            {
                CategoryName = addOrUpdateCategory.CategoryName,
                Description = addOrUpdateCategory.Description
            };

            try
            {
                await _categoryCommandService.AddOrUpdateCategoryAsync(category);
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
        public async Task<IActionResult> GetCurrentCategoryList()
        {
            IEnumerable<GetCurrentCategoryList> result;

            try
            {
                var categories = await _categoryQueryService.GetCategoriesAsync();

                result = categories
                    .Select(x => new GetCurrentCategoryList { CategoryName = x.CategoryName })
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(string categoryName)
        {
            if (categoryName is null)
            {
                throw new ArgumentNullException(nameof(categoryName));
            }

            try
            {
                await _categoryCommandService.DeleteCategoryAsync(categoryName);
            }
            catch (Exception)
            {
                throw;
            }

            return Ok();
        }
    }
}
