using FluentValidation;

namespace Mostra.Application.Products.GetProductById
{
    public class GetProductRequestValidator : AbstractValidator<GetProductRequestDto>
    {
        public GetProductRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("The product ID is invalid.");
        }
    }
}