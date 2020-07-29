using System.Collections.Generic;
using MediatR;

namespace CiSeCase.Core.Dtos.Request
{
    public class GetBasketRequest : IRequest<List<BasketDto>>
    {
        public int UserId { get; set; }
    }
}