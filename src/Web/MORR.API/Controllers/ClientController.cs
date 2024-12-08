using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MORR.Application.Pipelines.Products.Queries.GetProductsByFilter;
using MORR.Application.Pipelines.Users.Commands.Authentication;
using MORR.Application.Pipelines.Users.Commands.SaveCustomer;

namespace MORR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("getProductsByFilter")]
        public async Task<IActionResult> GetProductsByFilter([FromBody] GetProductsByFilterQuery query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("saveCustomer")]

        public async Task<IActionResult> SaveCustomer([FromBody] SaveCustomerCommand query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }
    }
}
