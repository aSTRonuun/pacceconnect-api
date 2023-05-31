using Application.Utils;
using Application.Utils.ResponseBase;
using Domain.CellDomain.Ports;
using MediatR;
using static Application.Utils.ResponseBase.Response;

namespace Application.CellApplication.Commands.Handlers
{
    public class UpdateStatusCellCommandHandler : IRequestHandler<UpdateStatusCellCommand, Response>
    {
        private readonly ICellRepository _cellRepository;

        public UpdateStatusCellCommandHandler(ICellRepository cellRepository)
        {
            _cellRepository = cellRepository;
        }

        public async Task<Response> Handle(UpdateStatusCellCommand request, CancellationToken cancellationToken)
        {
            var cellId = request.CellId;
            var action = request.Action;

            var cell = await _cellRepository.GetCellById(cellId);
            if (cell == null)
            {
                return new BadRequest("Cell not found", ErrorCodes.CELL_NOT_FOUND); 
            }

            cell.ChangeState(action);

            return new Success();

        }
    }
}
