using Application.UserApplication.Dtos;
using Application.Utils.ResponseBase;
using MediatR;

namespace Application.UserApplication.Commands
{
    public class UserAuthenticationCommand : IRequest<Response>
    {
        public UserAuthenticationCommand(LoginUserDto loginUser, string secretKey)
        {
            LoginUser = loginUser;
            SecretKey = secretKey;
        }

        public LoginUserDto LoginUser { get; set; }
        public string SecretKey { get; set; }
    }
}
