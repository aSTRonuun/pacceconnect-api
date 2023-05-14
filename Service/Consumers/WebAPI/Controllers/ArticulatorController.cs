using Application.ArticulatorApplication.Commands;
using Application.ArticulatorApplication.Dtos;
using Application.ArticulatorApplication.Queries;
using Application.ManagerApplication.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;


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
                InternalServerError =>
                {
                    this.ModelState.AddModelError("Message", InternalServerError.Message);
                    this.ModelState.AddModelError("ErrorCode", $"{InternalServerError.ErrorCodes}");

                    return this.NotFound(new ValidationProblemDetails(this.ModelState));
                });
        }

        [Authorize]
        [HttpGet("{articulatorId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(Status401Unauthorized)]
        [ProducesResponseType(Status404NotFound)]
        [ProducesResponseType(Status500InternalServerError)]
        public async Task<ActionResult<ArticulatorDto>> GetById(int articulatorId) 
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(new ValidationProblemDetails(this.ModelState));
            }

            var handlerResponse = await _mediator
                .Send(new GetArticulatorByIdQuery(articulatorId))
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
