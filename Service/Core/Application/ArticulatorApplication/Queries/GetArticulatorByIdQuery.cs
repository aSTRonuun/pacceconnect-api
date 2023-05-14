using Application.Utils.ResponseBase;
using MediatR;

namespace Application.ArticulatorApplication.Queries
{
    public class GetArticulatorByIdQuery : IRequest<Response>
    {
        public int Id { get; set; }

        public GetArticulatorByIdQuery(int id)
        {
            Id = id;
        }
    }
}
