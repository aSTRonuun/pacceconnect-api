using Application.Utils.ResponseBase;
using MediatR;
using Action = Domain.CellDomain.Enuns.Action;

namespace Application.CellApplication.Commands
{
    public class UpdateStatusCellCommand : IRequest<Response>
    {
        public readonly Action Action;
        public readonly int CellId;

        public UpdateStatusCellCommand(Action action, int cellId)
        {
            Action = action;
            CellId = cellId;
        }
    }
}
