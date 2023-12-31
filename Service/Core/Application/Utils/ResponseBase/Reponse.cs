﻿using OneOf;
using static Application.Utils.ResponseBase.Response;

namespace Application.Utils.ResponseBase
{
    public sealed partial class Response : OneOfBase<Success, BadRequest, InternalServerError>
    {

        public Response(OneOf<Success, BadRequest, InternalServerError> input) : base(input)
        {
        }

        public static implicit operator Response(Success success)
            => new Response(success);

        public static implicit operator Response(BadRequest badRequest)
            => new Response(badRequest);

        public static implicit operator Response(InternalServerError internalServerError)
            => new Response(internalServerError);
    }
}
