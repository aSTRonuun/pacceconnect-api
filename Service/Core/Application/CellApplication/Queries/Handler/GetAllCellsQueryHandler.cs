using Application.CellApplication.Dtos;
using Application.Utils;
using Application.Utils.ResponseBase;
using Domain.CellDomain.Ports;
using MediatR;
using static Application.Utils.ResponseBase.Response;

namespace Application.CellApplication.Queries.Handler
{
    public class GetAllCellsQueryHandler : IRequestHandler<GetAllCellsQuery, Response>
    {
        private ICellRepository _cellRepository;
        public GetAllCellsQueryHandler(ICellRepository cellRepository)
        {
            _cellRepository = cellRepository;
        }
        public async Task<Response> Handle(GetAllCellsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var cells = await _cellRepository.GetAllCells();
                var cellsDto = cells.Select(cell => CellDto.MapToDto(cell)).ToList();
                return new Success(cellsDto);
            }
            catch (Exception)
            {
                return new InternalServerError("Cells could not be founded", ErrorCodes.CELLPLAN_COULD_NOT_BE_STORE);
            }
        }
    }
}
