using Application.CellApplication.Dtos;
using Application.Utils.ResponseBase;
using MediatR;

namespace Application.CellApplication.Commands
{
    public class CreateCellCommand : IRequest<Response>
    {
        public CreateCellCommand(CellDto cellDto)
        {
            CellDto = cellDto;
        }

        public CellDto CellDto { get; set; }

    }
}
