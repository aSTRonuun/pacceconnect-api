using Application.Utils.ResponseBase;
using MediatR;

namespace Application.CellApplication.Queries
{
    public class GetAllCellsQuery : IRequest<Response>
    {
        public GetAllCellsQuery() { }
    }
}
