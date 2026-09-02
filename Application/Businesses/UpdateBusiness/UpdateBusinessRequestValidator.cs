using FluentValidation;

namespace Mostra.Application.Businesses.UpdateBusiness
{
    public class UpdateBusinessRequestValidator : AbstractValidator<UpdateBusinessRequestDto>
    {
        public UpdateBusinessRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("The business ID is invalid.");

            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("The maximum length must be less than 100 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Name));

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("The maximum length must be less than 500 characters.")
                .When(x => x.Description != null);

            RuleFor(x => x.LogoUrl)
                .MaximumLength(500).WithMessage("The maximum length must be less than 500 characters.")
                .When(x => x.LogoUrl != null);
        }
    }
}