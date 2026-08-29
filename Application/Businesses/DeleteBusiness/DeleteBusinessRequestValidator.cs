using FluentValidation;

namespace Mostra.Application.Businesses.DeleteBusiness
{
    public class DeleteBusinessRequestValidator : AbstractValidator<DeleteBusinessRequestDto>
    {
        public DeleteBusinessRequestValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("The business id is invalid.");
        }
    }
}