using FluentValidation;

namespace Mostra.Application.Business.CreateBussines
{
    public class CreateBusinessValidator : AbstractValidator<CreateBusinessRequestDto>
    {
        public CreateBusinessValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("The business name is obligatory")
                .MaximumLength(100).WithMessage("The maximum length mus't be less than 100");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("The maximum length mus'y be less than 500");

        }
    }
}
