using CiSeCase.Core.Dtos.Request;
using FluentValidation;

namespace CiSeCase.Infrastructure.Validation.Basket
{
    public class AddProductToBasketRequestValidator : AbstractValidator<AddProductToBasketRequest>
    {
        public AddProductToBasketRequestValidator()
        {
            RuleFor(x => x.UserId).Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .WithMessage("User id must be greater or equal to 1");

            RuleFor(x => x.ProductId).Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Product id must be greater or equal to 1");

            //If quantity is 0, remove from basket this product.

        }
    }
}