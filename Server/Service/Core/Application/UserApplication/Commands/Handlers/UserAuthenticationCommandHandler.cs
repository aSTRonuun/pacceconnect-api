using Application.UserApplication.Commands;
using Application.Service.Security;
using Application.UserApplication.Dtos;
using Application.Utils;
using Application.Utils.ResponseBase;
using Domain.UserDomain.Ports;
using MediatR;
using static Application.Utils.ResponseBase.Response;

namespace Application.UserManager.Commands.Handlers
{
    public class CreateManagerCommandHandler : IRequestHandler<CreateArticulatorCommand, Response>
    {
        private readonly IUserRepository _userRepository;

        public CreateManagerCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Response> Handle(CreateArticulatorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserByUserNameOrEmail(request.LoginUser.EmailOrUserName);
                if (user == null)
                {
                    return new BadRequest("User not found", ErrorCodes.USER_NOT_FOUND);
                }

                if (!user.VerifyPasswordHash(request.LoginUser.Password))
                {
                    return new BadRequest("User password is incorrect", ErrorCodes.USER_PASSWORD_INCORRECT);
                }

                var tokenDto = new TokenDto
                {
                    Token = TokenService.GenerateToken(user, request.SecretKey),
                };

                return new Success(tokenDto);

            }
            catch (Exception)
            {
                return new BadRequest("Unable to athenticate user", ErrorCodes.UNABLE_TO_ATHENTICATE_USER);
            }
        }
    }
}
