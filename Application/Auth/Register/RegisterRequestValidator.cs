using FluentValidation;

namespace Mostra.Application.Auth.Register
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequestDto>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("The email is obligatory.")
                .EmailAddress().WithMessage("The email format is invalid.")
                .MaximumLength(100).WithMessage("The maximum length must be less than 100 characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("The password is obligatory.")
                .MinimumLength(6).WithMessage("The password must be at least 6 characters long.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("The name is obligatory.")
                .MaximumLength(100).WithMessage("The maximum length must be less than 100 characters.");
        }
    }
}