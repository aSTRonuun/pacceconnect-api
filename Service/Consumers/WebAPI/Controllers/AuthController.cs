using Application.UserApplication.Commands;
using Application.UserApplication.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OneOf.Types;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
     {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public AuthController(IMediator metiator, IConfiguration configuration)
        {
            _mediator = metiator;
            _configuration = configuration;
        }

        [HttpPost("authenticate")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(TokenDto), Status200OK)]
        [ProducesResponseType(Status401Unauthorized)]
        [ProducesResponseType(Status500InternalServerError)]
        public async Task<ActionResult<TokenDto>> Authenticate([FromBody] LoginUserDto userDto)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(new ValidationProblemDetails(this.ModelState));
            }

            var secret = _configuration.GetSection("Jwt:SecretKey").Value;

            var handlerResponse = await _mediator
                .Send(new UserAuthenticationCommand(userDto, secret))
                .ConfigureAwait(false);

            return handlerResponse.Match<ActionResult>(
                Success => this.Ok(Success.Dto),
                BadRequest =>
                {
                    this.ModelState.AddModelError("Message", BadRequest.Message);
                    this.ModelState.AddModelError("ErrorCode", $"{BadRequest.ErrorCodes}");

                    return this.Unauthorized(new ValidationProblemDetails(this.ModelState));
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
