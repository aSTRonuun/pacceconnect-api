using Application.Utils.ResponseBase;
using MediatR;

namespace Application.CellApplication.Queries
{
    public class GetCellByUserIdQuery : IRequest<Response>
    {
        public GetCellByUserIdQuery(int articulatorId)
        {
            ArticulatorId = articulatorId;
        }
        public int ArticulatorId { get; set; }
    }
}
