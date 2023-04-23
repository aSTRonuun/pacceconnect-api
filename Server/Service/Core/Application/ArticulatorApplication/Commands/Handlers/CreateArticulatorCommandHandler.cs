using Application.Service.Security;
using Application.UserApplication.Dtos;
using Application.Utils;
using Application.Utils.ResponseBase;
using MediatR;
using static Application.Utils.ResponseBase.Response;
using Domain.ArticulatorDomain.Ports;
using Application.ArticulatorApplication.Commands;
using Application.ArticulatorApplication.Dtos;

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

                await _articulatorRepository.Create(articulator);

                articulatorDto.Id = articulator.Id;

                return new Success(articulatorDto);
            }
            catch (Exception)
            {
                return new BadRequest("Articulator could not be storage", ErrorCodes.ARTICULATOR_COULD_NOT_BE_STORAGE);
            }
        }
    }
}
