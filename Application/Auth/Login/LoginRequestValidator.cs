using FluentValidation;

namespace Mostra.Application.Auth.Login
{
    public class LoginRequestValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("The email is obligatory.")
                .EmailAddress().WithMessage("The email format is invalid.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("The password is obligatory.");
        }
    }
}