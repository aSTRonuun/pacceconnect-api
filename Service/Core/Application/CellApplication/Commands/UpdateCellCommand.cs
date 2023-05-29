using Application.CellApplication.Dtos;
using Application.Utils.ResponseBase;
using MediatR;

namespace Application.CellApplication.Commands
{
    public class UpdateCellCommand : IRequest<Response>
    {
        public CellDto CellDto { get; set; }

        public UpdateCellCommand(CellDto cellDto)
        {
            CellDto = cellDto;
        }
    }
}
