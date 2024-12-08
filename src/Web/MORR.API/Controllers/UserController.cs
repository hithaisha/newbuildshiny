using MediatR;
using Microsoft.AspNetCore.Mvc;
using MORR.Application.Pipelines.Users.Queries.GetAllUsersByFilter;

namespace MORR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("getAllUsersByFilter")]
        public async Task<IActionResult> GetAllUsersByFilter([FromBody] GetAllUsersByFilterQuery query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }
    }
}
