using Application.Utils.IDtoBase;

namespace Application.Utils.ResponseBase
{
    public sealed partial class Response
    {
        public sealed class Success
        {
            public Success()
            {
            }

            public Success(IDto dto)
            {
                this.Dto = dto;
            }

            public Success(IEnumerable<IDto> dtos)
            {
                this.Dtos = dtos;
            }

            public Success(string message)
            {
                this.Message = message;
            }

            public IDto Dto { get; set; }
            public IEnumerable<IDto> Dtos { get; set; }
            public string Message { get; set; }
        }
    }
}
