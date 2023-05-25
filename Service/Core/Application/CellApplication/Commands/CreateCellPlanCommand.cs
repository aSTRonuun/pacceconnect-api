using Application.CellApplication.Dtos;
using Application.Utils.ResponseBase;
using MediatR;

namespace Application.CellApplication.Commands
{
    public class CreateCellPlanCommand : IRequest<Response>
    {
        public CreateCellPlanCommand(CellPlanDto cellPlanDto)
        {
            CellPlanDto = cellPlanDto;
        }
        public CellPlanDto CellPlanDto { get; set; }
    }
}
