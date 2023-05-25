using Application.CellApplication.Commands;
using Application.CellApplication.Dtos;
using Application.CellApplication.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CellController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CellController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(Status401Unauthorized)]
        [ProducesResponseType(Status404NotFound)]
        [ProducesResponseType(Status500InternalServerError)]
        public async Task<IActionResult> CreateCell([FromBody] CellDto cellDto)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(new ValidationProblemDetails(this.ModelState));
            }

            var handlerReponse = await _mediator
                .Send(new CreateCellCommand(cellDto))
                .ConfigureAwait(false);

            return handlerReponse.Match<ActionResult>(
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

        [HttpGet("{articulatorId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(Status401Unauthorized)]
        [ProducesResponseType(Status404NotFound)]
        [ProducesResponseType(Status500InternalServerError)]
        public async Task<IActionResult> GetByArticulatorId(int articulatorId)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(new ValidationProblemDetails(this.ModelState));
            }

            var handlerReponse = await _mediator
                .Send(new GetCellByUserIdQuery(articulatorId))
                .ConfigureAwait(false);

            return handlerReponse.Match<ActionResult>(
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

        [HttpGet("GetAll")]
        [ProducesResponseType(200)]
        [ProducesResponseType(Status401Unauthorized)]
        [ProducesResponseType(Status404NotFound)]
        [ProducesResponseType(Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(new ValidationProblemDetails(this.ModelState));
            }

            var handlerReponse = await _mediator
                .Send(new GetAllCellsQuery())
                .ConfigureAwait(false);

            return handlerReponse.Match<ActionResult>(
                success => this.Ok(success.Dtos),
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
