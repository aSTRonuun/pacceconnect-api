using Application.Utils.IDtoBase;

namespace Application.UserApplication.Dtos
{
    public class TokenDto : IDto
    {
        public string Token { get; set; } = string.Empty;
    }
}
