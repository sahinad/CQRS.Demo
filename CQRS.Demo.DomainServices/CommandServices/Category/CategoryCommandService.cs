using CQRS.Demo.Domain.Commands;
using CQRS.Demo.Domain.Commands.Category;
using CQRS.Demo.DomainServices.QueryServices.Category;
using CQRS.Demo.DomainServices.Validators;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace CQRS.Demo.DomainServices.CommandServices.Category
{
    public class CategoryCommandService : ICategoryCommandService
    {
        private readonly ICommandFactory _commandFactory;
        private readonly ICategoryQueryService _categoryQueryService;
        private readonly ICategoryCommand _categoryCommand;

        public CategoryCommandService(ICommandFactory commandFactory, ICategoryQueryService categoryQueryService)
        {
            _commandFactory = commandFactory ?? throw new ArgumentNullException(nameof(commandFactory));
            _categoryQueryService = categoryQueryService ?? throw new ArgumentNullException(nameof(categoryQueryService));
            _categoryCommand = _commandFactory.CreateCommand<ICategoryCommand>();
        }

        public async Task AddOrUpdateCategoryAsync(Domain.Models.Category category)
        {
            var validator = new CategoryValidator();
            ValidationResult result = validator.Validate(category);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            var categoryId = await _categoryQueryService.CheckIfCategoryExists(category.CategoryName);

            if (categoryId > 0)
            {
                category.Id = categoryId;
            }

            await _categoryCommand.AddOrUpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(string categoryName)
        {
            var category = await _categoryQueryService.GetCategoryByNameAsync(categoryName);

            await _categoryCommand.DeleteAsync(category);
        }
    }
}
