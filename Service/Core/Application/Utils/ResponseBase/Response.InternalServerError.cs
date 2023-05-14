namespace Application.Utils.ResponseBase
{
    public sealed partial class Response
    {
        public sealed partial class InternalServerError
        {
            public InternalServerError(string message, ErrorCodes errorCodes)
            {
                this.Message = message;
                this.ErrorCodes = errorCodes;
            }

            public string Message { get; set; }

            public ErrorCodes ErrorCodes { get; set; }
        }
    }
}
