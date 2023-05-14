using Application.Utils.IDtoBase;
using Domain.UserDomain.Enuns;

namespace Application.UserApplication.Dtos
{
    public class TokenDto : IDto
    {
        public int UserId { get; set; }
        public Roles Role { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
