using Application.Utils;
using Application.Utils.ResponseBase;
using Domain.CellDomain.Ports;
using MediatR;
using static Application.Utils.ResponseBase.Response;
using Action = Domain.CellDomain.Enuns.Action;

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
            try
            {
                var cellId = request.CellId;
                var action = request.Action;

                var cell = await _cellRepository.GetCellById(cellId);
                if (cell == null)
                {
                    return new BadRequest("Cell not found", ErrorCodes.CELL_NOT_FOUND);
                }

                if (action == Action.Approve && !cell.CellPlan.PlanIsCompleted())
                {
                    return new BadRequest("Unable to update status", ErrorCodes.CELL_CHANGE_STATE_NOT_IS_POSSIBLE);
                }

                cell.ChangeState(action);

                await cell.Save(_cellRepository);

                return new Success();
            }
            catch (Exception)
            {
                return new BadRequest("Unable to update status", ErrorCodes.CELL_CHANGE_STATE_NOT_IS_POSSIBLE);
            }

        }
    }
}
