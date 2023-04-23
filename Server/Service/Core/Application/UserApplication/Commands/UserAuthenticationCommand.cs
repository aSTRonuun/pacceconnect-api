using Application.UserApplication.Dtos;
using Application.Utils.ResponseBase;
using MediatR;

namespace Application.UserApplication.Commands
{
    public class CreateArticulatorCommand : IRequest<Response>
    {
        public CreateArticulatorCommand(LoginUserDto loginUser, string secretKey)
        {
            LoginUser = loginUser;
            SecretKey = secretKey;
        }

        public LoginUserDto LoginUser { get; set; }
        public string SecretKey { get; set; }
    }

}
