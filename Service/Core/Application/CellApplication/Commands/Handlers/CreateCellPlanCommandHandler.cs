using Application.CellApplication.Dtos;
using Application.Utils;
using Application.Utils.ResponseBase;
using Domain.CellDomain.Ports;
using MediatR;
using static Application.Utils.ResponseBase.Response;

namespace Application.CellApplication.Commands.Handlers
{
    public class CreateCellPlanCommandHandler : IRequestHandler<CreateCellPlanCommand, Response>
    {
        private ICellRepository _cellRepository;

        public CreateCellPlanCommandHandler(ICellRepository cellRepository)
        {
            _cellRepository = cellRepository;
        }

        public async Task<Response> Handle(CreateCellPlanCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var cellPlanDto = request.CellPlanDto;
                var cellPlan = CellPlanDto.MapToEntity(cellPlanDto);

                //await cellPlan.Save(_cellRepository);

                cellPlanDto.Id = cellPlan.Id;

                return new Success(cellPlanDto);
            }
            catch (Exception)
            {
                return new BadRequest("Cell Plan could not be storage", ErrorCodes.CELLPLAN_COULD_NOT_BE_STORE);
            }
        }
    }
}
