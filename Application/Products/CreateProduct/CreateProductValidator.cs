using FluentValidation;

namespace Mostra.Application.Products.CreateProduct
{
    public class CreateProductValidator : AbstractValidator<CreateProductRequestDto>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("The product name is obligatory.")
                .MinimumLength(5).WithMessage("The minimum length must be greater than 5.");


            RuleFor(x => x.ProductDescription)
                .NotEmpty().WithMessage("The product description is obligatory.")
                .MinimumLength(50).WithMessage("The minimum length must be greater than 50.")
                .MaximumLength(500).WithMessage("The maximum length must be less than 500.");


            RuleFor(x => x.ProductPrice)
                .GreaterThan(0).WithMessage("The price length must be greater than 0.");

            RuleFor(x => x.BusinessId)
                .GreaterThan(0).WithMessage("You must specify a valid business.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("You must specify a category.");



        }
    }
}
