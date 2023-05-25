using Application.CellApplication.Dtos;
using Application.Utils;
using Application.Utils.ResponseBase;
using Domain.CellDomain.Ports;
using MediatR;
using static Application.Utils.ResponseBase.Response;

namespace Application.CellApplication.Commands.Handlers
{
    public class CreateCellCommandHandler : IRequestHandler<CreateCellCommand, Response>
    {
        private ICellRepository _cellRepository;

        public CreateCellCommandHandler(ICellRepository cellRepository)
        {
            _cellRepository = cellRepository;
        }

        public async Task<Response> Handle(CreateCellCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var cellDto = request.CellDto;
                var cell = CellDto.MapToEntity(cellDto);

                await cell.Save(_cellRepository);

                cellDto.Id = cell.Id;
                cellDto.Plan.Id = cell.CellPlan.Id;

                return new Success(cellDto);
            }
            catch (Exception)
            {
                return new BadRequest("Cell could not be storage", ErrorCodes.CELL_COULD_NOT_BE_STORAGE);
            }
        }
    }
}
