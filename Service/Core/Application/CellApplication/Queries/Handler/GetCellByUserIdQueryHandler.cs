using Application.CellApplication.Dtos;
using Application.Utils;
using Application.Utils.ResponseBase;
using Domain.CellDomain.Ports;
using MediatR;
using static Application.Utils.ResponseBase.Response;

namespace Application.CellApplication.Queries.Handler
{
    public class GetCellByUserIdQueryHandler : IRequestHandler<GetCellByUserIdQuery, Response>
    {
        private ICellRepository _cellRepository;

        public GetCellByUserIdQueryHandler(ICellRepository cellRepository)
        {
            _cellRepository = cellRepository;
        }

        public async Task<Response> Handle(GetCellByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var cell = await _cellRepository.GetCellByArticulatorId(request.ArticulatorId);
                if (cell == null)
                {
                    return new BadRequest("Cell not found", ErrorCodes.CELL_NOT_FOUND);
                }
                var cellDto = CellDto.MapToDto(cell);

                return new Success(cellDto);
            }
            catch (Exception)
            {
                return new InternalServerError("Cell could not be founded", ErrorCodes.CELLPLAN_COULD_NOT_BE_STORE);
            }
        }
    }
}
