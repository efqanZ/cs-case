using System.Threading.Tasks;
using CiSeCase.Core.Dtos.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CiSeCase.Api.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] AddProductToBasketRequest request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get(int userId)
        {
            var request = new GetBasketRequest { UserId = userId };
            var response = await _mediator.Send(request);
            return Ok(response);
        }

    }
}