using Application.Service.Security;
using Application.UserApplication.Dtos;
using Application.Utils;
using Application.Utils.ResponseBase;
using Domain.UserDomain.Ports;
using MediatR;
using static Application.Utils.ResponseBase.Response;

namespace Application.UserApplication.Commands.Handlers
{
    public class UserAuthenticationCommandHandler : IRequestHandler<UserAuthenticationCommand, Response>
    {
        private readonly IUserRepository _userRepository;

        public UserAuthenticationCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Response> Handle(UserAuthenticationCommand request, CancellationToken cancellationToken)
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
                    return new BadRequest("User password is incorrect", ErrorCodes.USER_INCORRECT_Credentials);
                }

                var tokenDto = new TokenDto
                {
                    UserId = user.Id,
                    Role = user.Role,
                    Token = TokenService.GenerateToken(user, request.SecretKey),
                };

                return new Success(tokenDto);

            }
            catch (Exception)
            {
                return new InternalServerError("Unable to athenticate user", ErrorCodes.UNABLE_TO_ATHENTICATE_USER);
            }
        }
    }
}
