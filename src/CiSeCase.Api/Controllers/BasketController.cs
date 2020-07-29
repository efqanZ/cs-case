using System.Threading.Tasks;
using CiSeCase.Core.Dtos.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CiSeCase.Api.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> AddToBasket([FromBody] AddProductToBasketRequest request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}