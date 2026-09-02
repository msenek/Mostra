using FluentValidation;

namespace Mostra.Application.Categories.UpdateCategory
{
    public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequestDto>
    {
        public UpdateCategoryRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("The category ID is invalid.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("The category name is obligatory.")
                .MaximumLength(50).WithMessage("The maximum length must be less than 50 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Name));

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("The maximum length must be less than 500 characters.")
                .When(x => x.Description != null);
        }
    }
}