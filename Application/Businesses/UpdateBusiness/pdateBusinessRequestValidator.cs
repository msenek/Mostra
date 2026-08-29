using FluentValidation;

namespace Mostra.Application.Businesses.UpdateBusiness
{
    public class UpdateBusinessRequestValidator : AbstractValidator<UpdateBusinessRequestDto>
    {
        public UpdateBusinessRequestValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Invalid business id.");
            RuleFor(x => x.Name).MaximumLength(100).WithMessage("the business name is too long.");
        }
    }
}