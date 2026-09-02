using FluentValidation;

namespace Mostra.Application.Categories.CreateCategory
{
    public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequestDto>
    {
        public CreateCategoryRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("The category name is obligatory.")
                .MaximumLength(50).WithMessage("The maximum length must be less than 50 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("The maximum length must be less than 500 characters.")
                .When(x => x.Description != null);

            RuleFor(x => x.BusinessId)
                .GreaterThan(0).WithMessage("You must specify a valid business.");
        }
    }
}