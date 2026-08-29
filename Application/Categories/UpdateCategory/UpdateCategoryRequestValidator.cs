using FluentValidation;

namespace Mostra.Application.Categories.UpdateCategory
{
    public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequestDto>
    {
        public UpdateCategoryRequestValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("The category id is invalid");
            RuleFor(x => x.Name).NotEmpty().WithMessage("The category name is obligatory").MaximumLength(50);
        }
    }
}