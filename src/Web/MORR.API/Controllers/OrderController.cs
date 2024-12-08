using MediatR;
using Microsoft.AspNetCore.Mvc;
using MORR.Application.DTOs.OrderDTOs;
using MORR.Application.Pipelines.Orders.Commands.SaveOrder;

namespace MORR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("saveOrder")]
        public async Task<IActionResult> SaveOrder([FromBody] OrderDTO dto)
        {
            var response = await _mediator.Send(new SaveOrderCommand(dto));

            return Ok(response);
        }
    }
}
