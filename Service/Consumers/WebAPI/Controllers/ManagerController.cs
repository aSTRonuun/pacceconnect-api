using Application.ArticulatorApplication.Dtos;
using Application.ManagerApplication.Commands;
using Application.ManagerApplication.Dtos;
using Application.ManagerApplication.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

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
                InternalServerError =>
                {
                    this.ModelState.AddModelError("Message", InternalServerError.Message);
                    this.ModelState.AddModelError("ErrorCode", $"{InternalServerError.ErrorCodes}");

                    return this.NotFound(new ValidationProblemDetails(this.ModelState));
                });
        }

        [Authorize]
        [HttpGet("{managerId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(Status401Unauthorized)]
        [ProducesResponseType(Status404NotFound)]
        [ProducesResponseType(Status500InternalServerError)]
        public async Task<ActionResult<ManagerDto>> GetById(int managerId)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(new ValidationProblemDetails(this.ModelState));
            }

            var handlerResponse = await _mediator
                .Send(new GetManagerByIdQuery(managerId))
                .ConfigureAwait(false);

            return handlerResponse.Match<ActionResult>(
                success => this.Ok(success.Dto),
                badRequest =>
                {
                    this.ModelState.AddModelError("Message", badRequest.Message);
                    this.ModelState.AddModelError("ErrorCode", $"{badRequest.ErrorCodes}");

                    return this.BadRequest(new ValidationProblemDetails(this.ModelState));
                },
                InternalServerError =>
                {
                    this.ModelState.AddModelError("Message", InternalServerError.Message);
                    this.ModelState.AddModelError("ErrorCode", $"{InternalServerError.ErrorCodes}");

                    return this.NotFound(new ValidationProblemDetails(this.ModelState));
                });
        }
    }
}
