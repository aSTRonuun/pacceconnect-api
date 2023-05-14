using Application.Utils;
using Application.Utils.ResponseBase;
using MediatR;
using static Application.Utils.ResponseBase.Response;
using Domain.ManagerDomain.Ports;
using Application.ManagerApplication.Dtos;
using Domain.ManagerDomain.Exceptions;
using Domain.UserDomain.Exceptions;

namespace Application.ManagerApplication.Commands.Handlers
{
    public class CreateManagerCommandHandler : IRequestHandler<CreateManagerCommand, Response>
    {
        private readonly IManagerRepository _managerRepository;

        public CreateManagerCommandHandler(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }

        public async Task<Response> Handle(CreateManagerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var managerDto = request.CreateManagerDto;
                var manager = CreateManagerDto.MapToEntity(managerDto);

                manager.CreatePasswordHash(managerDto.Password);

                await manager.Save(_managerRepository);
                
                managerDto.Id = manager.Id;

                return new Success(managerDto);
            }
            catch (ManagerMissingRequiredInformationException)
            {
                return new BadRequest("Manager no has required information", ErrorCodes.MANAGER_MISSING_REQUIRED_INFORMATION);
            }
            catch (UserPasswordLengthException)
            {
                return new BadRequest("User password length is invalid", ErrorCodes.USER_LENGTH_IS_INVALID);
            }
            catch (Exception)
            {
                return new InternalServerError("Manager Could not be storage", ErrorCodes.MANAGER_COULD_NOT_BE_STORAGE);
            }
        }
    }
}
