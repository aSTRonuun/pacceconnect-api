using Application.ArticulatorApplication.Dtos;
using Application.Utils;
using Application.Utils.ResponseBase;
using Domain.ArticulatorDomain.Ports;
using MediatR;
using static Application.Utils.ResponseBase.Response;

namespace Application.ArticulatorApplication.Queries.Handlers
{
    public class GetArticulatorByIdQueyHandler : IRequestHandler<GetArticulatorByIdQuery, Response>
    {
        private readonly IArticulatorRepository _articulatorRepository;

        public GetArticulatorByIdQueyHandler(IArticulatorRepository articulatorRepository)
        {
            _articulatorRepository = articulatorRepository;
        }

        public async Task<Response> Handle(GetArticulatorByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var id = request.Id;
                var articulator = await _articulatorRepository.GetById(id);
                if (articulator == null)
                {
                    return new BadRequest("Articulator not found", ErrorCodes.ARTICULATOR_NOT_FOUND);
                }

                var articulatorDto = ArticulatorDto.MapToDto(articulator);

                return new Success(articulatorDto);
            }
            catch (Exception)
            {
                return new InternalServerError("Articulator could not be found", ErrorCodes.ARTICULATOR_COULD_NOT_BE_FOUND);
            }
        }
    }
}
