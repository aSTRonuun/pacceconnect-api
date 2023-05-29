using Application.CellApplication.Dtos;
using Application.Utils;
using Application.Utils.ResponseBase;
using Domain.CellDomain.Exceptions;
using Domain.CellDomain.Ports;
using MediatR;
using static Application.Utils.ResponseBase.Response;

namespace Application.CellApplication.Commands.Handlers
{
    public class UpdateCellPlanCommandHandler : IRequestHandler<UpdateCellCommand, Response>
    {
        private readonly ICellRepository _cellRepository;

        public UpdateCellPlanCommandHandler(ICellRepository cellRepository)
        {
            _cellRepository = cellRepository;
        }

        public async Task<Response> Handle(UpdateCellCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var cellDto = request.CellDto;
                var cell = CellDto.MapToEntity(cellDto);

                await cell.Save(_cellRepository);

                return new Success(cellDto);
            }
            catch (MissingRequiredInformationException)
            {
                return new BadRequest("Cell no has required information", ErrorCodes.CELL_MISSING_REQUIRED_INFORMATIONS);
            }
            catch (MissingArticulatorEntityRequiredInformationException)
            {
                return new BadRequest("Cell no has articulator required information", ErrorCodes.CELL_MISSING_ARTICULATOR_INFORMATION);
            }
            catch (Exception)
            {
                return new BadRequest("Cell could not be storage", ErrorCodes.CELL_COULD_NOT_BE_STORAGE);
            }
        }
    }
}
