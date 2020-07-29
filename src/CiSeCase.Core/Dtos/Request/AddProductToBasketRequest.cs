using MediatR;

namespace CiSeCase.Core.Dtos.Request
{
    public class AddProductToBasketRequest : IRequest<bool>
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}