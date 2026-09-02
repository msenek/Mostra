using FluentValidation;

namespace Mostra.Application.Categories.DeleteCategory
{
    public class DeleteCategoryRequestValidator : AbstractValidator<DeleteCategoryRequestDto>
    {
        public DeleteCategoryRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("The category ID is invalid.");
        }
    }
}