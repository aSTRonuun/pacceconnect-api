using Application.Utils.ResponseBase;
using MediatR;

namespace Application.ManagerApplication.Queries
{
    public class GetManagerByIdQuery : IRequest<Response>
    {
        public int Id { get; set; }

        public GetManagerByIdQuery(int id)
        {
            Id = id;
        }
    }
}
