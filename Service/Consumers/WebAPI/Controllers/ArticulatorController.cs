using Application.ArticulatorApplication.Commands;
using Application.ArticulatorApplication.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticulatorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ArticulatorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] CreateArticulatorDto articulatorDto)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(new ValidationProblemDetails(this.ModelState));
            }

            var handlerReponse = await _mediator
                .Send(new CreateArticulatorCommand(articulatorDto))
                .ConfigureAwait(false);

            return handlerReponse.Match<ActionResult>(
                Success => this.Created("", Success.Dto),
                BadRequest =>
                {
                    this.ModelState.AddModelError("Message", BadRequest.Message);
                    this.ModelState.AddModelError("ErrorCode", $"{ BadRequest.ErrorCodes}");

                    return this.BadRequest(new ValidationProblemDetails(this.ModelState));
                },
                NotFound => this.NotFound(NotFound));
        }
    }
}
