using Application.ManagerApplication.Commands;
using Application.ManagerApplication.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ManagerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ManagerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] CreateManagerDto managerDto)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(new ValidationProblemDetails(this.ModelState));
            }

            var handlerReponse = await _mediator
                .Send(new CreateManagerCommand(managerDto))
                .ConfigureAwait(false);

            return handlerReponse.Match<ActionResult>(
                Success => this.Created("", Success.Dto),
                BadRequest =>
                {
                    this.ModelState.AddModelError("Message", BadRequest.Message);
                    this.ModelState.AddModelError("ErrorCode", $"{BadRequest.ErrorCodes}");

                    return this.BadRequest(new ValidationProblemDetails(this.ModelState));
                },
                NotFound => this.NotFound(NotFound));
        }
    }
}
