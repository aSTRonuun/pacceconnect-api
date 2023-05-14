using Application.Utils;
using Application.Utils.ResponseBase;
using MediatR;
using static Application.Utils.ResponseBase.Response;
using Domain.ArticulatorDomain.Ports;
using Application.ArticulatorApplication.Dtos;
using Domain.UserDomain.Exceptions;

namespace Application.ArticulatorApplication.Commands.Handlers
{
    public class CreateArticulatorCommandHandler : IRequestHandler<CreateArticulatorCommand, Response>
    {
        private readonly IArticulatorRepository _articulatorRepository;

        public CreateArticulatorCommandHandler(IArticulatorRepository articulatorRepository)
        {
            _articulatorRepository = articulatorRepository;
        }

        public async Task<Response> Handle(CreateArticulatorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var articulatorDto = request.CreateArticulatorDto;
                var articulator = CreateArticulatorDto.MapToEntity(articulatorDto);

                articulator.CreatePasswordHash(articulatorDto.Password);

                await articulator.Save(_articulatorRepository);

                articulatorDto.Id = articulator.Id;

                return new Success(articulatorDto);
            }
            catch (ArticulatorMissingRequiredInformation)
            {
                return new BadRequest("Articulator no has required infomations", ErrorCodes.ARTICULATOR_MISSING_REQUIRED_INFORMATION);
            }
            catch (UserPasswordLengthException)
            {
                return new BadRequest("User password length is invalid", ErrorCodes.USER_LENGTH_IS_INVALID);
            }
            catch (Exception)
            {
                return new InternalServerError("Articulator could not be storage", ErrorCodes.ARTICULATOR_COULD_NOT_BE_STORAGE);
            }
        }
    }
}
