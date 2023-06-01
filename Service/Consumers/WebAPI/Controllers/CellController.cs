using Application.CellApplication.Commands;
using Application.CellApplication.Dtos;
using Application.CellApplication.Queries;
using Domain.CellDomain.Enuns;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Action = Domain.CellDomain.Enuns.Action;

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

            var handlerResponse = await _mediator
                .Send(new CreateCellCommand(cellDto))
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

            var handlerResponse = await _mediator
                .Send(new GetCellByUserIdQuery(articulatorId))
                .ConfigureAwait(false);

            return handlerResponse.Match<ActionResult>(
                success => this.Ok(success.Dto),
                badRequest =>
                {
                    this.ModelState.AddModelError("Message", badRequest.Message);
                    this.ModelState.AddModelError("ErrorCode", $"{badRequest.ErrorCodes}");

                    return this.NotFound(new ValidationProblemDetails(this.ModelState));
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

            var handlerResponse = await _mediator
                .Send(new GetAllCellsQuery())
                .ConfigureAwait(false);

            return handlerResponse.Match<ActionResult>(
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

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(Status401Unauthorized)]
        [ProducesResponseType(Status404NotFound)]
        [ProducesResponseType(Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] CellDto cellDto)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(new ValidationProblemDetails(this.ModelState));
            }

            var handlerResponse = await _mediator
                .Send(new UpdateCellCommand(cellDto))
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

        [HttpPut("status/{cellId}/action/{intention}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(Status401Unauthorized)]
        [ProducesResponseType(Status404NotFound)]
        [ProducesResponseType(Status500InternalServerError)]
        public async Task<IActionResult> UpdateStatus(int cellId, Action intention)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(new ValidationProblemDetails(this.ModelState));
            }

            var handlerResponse = await _mediator
                .Send(new UpdateStatusCellCommand(intention, cellId))
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
