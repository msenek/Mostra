using FluentValidation;
using Microsoft.Extensions.Options;

namespace Mostra.Application.Products.UpdateProduct
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductRequestDto>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("The product id must be a valid number greater than 0");

            RuleFor(x => x.ProductName)
                .MinimumLength(5).WithMessage("If you send the product name, that name must be greater than 5")
                .When(x => !string.IsNullOrEmpty(x.ProductName));

            RuleFor(x => x.ProductDescription)
                .MinimumLength(50).WithMessage("The product description must be greater than 50")
                .MaximumLength(500).WithMessage("The product description must be less than 500")
                .When(x => !string.IsNullOrEmpty(x.ProductName));

            RuleFor(x => x.ProductPrice)
                .GreaterThan(0).WithMessage("If you send the prodcut price, that price must be grater than 0")
                .When(x => x.ProductPrice.HasValue);
        }
    }
}
