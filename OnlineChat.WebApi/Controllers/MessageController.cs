using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineChat.Application.UseCases.ToDoList;

namespace OnlineChat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessageController(
        IMediator mediator
        ) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPut("mark-as-read")]
        public async Task<IActionResult> MarkAsRead([FromBody] string id)
        {
            try
            {
                return Ok(await _mediator.Send(new MarkAsSeenMessageCommand(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
