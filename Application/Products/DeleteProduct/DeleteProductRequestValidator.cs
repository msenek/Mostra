using FluentValidation;

namespace Mostra.Application.Products.DeleteProduct
{
    public class DeleteProductRequestValidator : AbstractValidator<DeleteProductRequestDto>
    {
        public DeleteProductRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("The product ID is invalid.");
        }
    }
}