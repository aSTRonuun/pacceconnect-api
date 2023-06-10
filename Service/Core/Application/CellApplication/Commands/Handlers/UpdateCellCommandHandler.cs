using Application.CellApplication.Dtos;
using Application.Utils;
using Application.Utils.ResponseBase;
using Domain.CellDomain.Exceptions;
using Domain.CellDomain.Ports;
using MediatR;
using static Application.Utils.ResponseBase.Response;

namespace Application.CellApplication.Commands.Handlers
{
    public class UpdateCellCommandHandler : IRequestHandler<UpdateCellCommand, Response>
    {
        private readonly ICellRepository _cellRepository;

        public UpdateCellCommandHandler(ICellRepository cellRepository)
        {
            _cellRepository = cellRepository;
        }

        public async Task<Response> Handle(UpdateCellCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var cellDto = request.CellDto;
                var cell = CellDto.MapToEntity(cellDto);

                var cellStatus = await _cellRepository.GetCellById(cellDto.Id);
                if (cellStatus == null)
                {
                    return new BadRequest("Cell not found", ErrorCodes.CELL_NOT_FOUND);
                }

                cell.Status = cellStatus.Status;
                cellDto.Status = cellStatus.Status;

                await cell.Save(_cellRepository);

                return new Success(cellDto);
            }
            catch (CellMissingRequiredInformationException)
            {
                return new BadRequest("Cell no has required information", ErrorCodes.CELL_MISSING_REQUIRED_INFORMATIONS);
            }
            catch (CellMissingArticulatorEntityRequiredInformationException)
            {
                return new BadRequest("Cell no has articulator required information", ErrorCodes.CELL_MISSING_ARTICULATOR_INFORMATION);
            }
            catch (Exception)
            {
                return new InternalServerError("Cell could not be storage", ErrorCodes.CELL_COULD_NOT_BE_STORAGE);
            }
        }
    }
}
