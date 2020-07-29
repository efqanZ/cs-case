using CiSeCase.Core.Dtos.Request;
using FluentValidation;

namespace CiSeCase.Infrastructure.Validation.Basket
{
    public class GetBasketRequestValidator : AbstractValidator<GetBasketRequest>
    {
        public GetBasketRequestValidator()
        {
            RuleFor(x => x.UserId).Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .WithMessage("User id must be greater or equal to 1");
        }
    }
}