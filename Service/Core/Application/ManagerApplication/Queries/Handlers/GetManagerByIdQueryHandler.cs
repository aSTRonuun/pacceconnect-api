using Application.ManagerApplication.Dtos;
using Application.Utils;
using Application.Utils.ResponseBase;
using Domain.ManagerDomain.Ports;
using MediatR;
using static Application.Utils.ResponseBase.Response;

namespace Application.ManagerApplication.Queries.Handlers
{
    public class GetManagerByIdQueryHandler : IRequestHandler<GetManagerByIdQuery, Response>
    {
        private readonly IManagerRepository _managerRepository;

        public GetManagerByIdQueryHandler(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }

        public async Task<Response> Handle(GetManagerByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var id = request.Id;
                var manager = await _managerRepository.GetById(id);
                if (manager == null)
                {
                    return new BadRequest("Manager not found", ErrorCodes.MANAGER_NOT_FOUND);
                }
                
                var managerDto = ManagerDto.MapToDto(manager);

                return new Success(managerDto);

            }
            catch (Exception)
            {
                return new InternalServerError("Manager could not be found", ErrorCodes.MANAGER_COULD_NOT_BE_FOUND);
            }
        }
    }
}
