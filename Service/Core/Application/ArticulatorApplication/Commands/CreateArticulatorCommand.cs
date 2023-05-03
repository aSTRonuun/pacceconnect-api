using Application.ArticulatorApplication.Dtos;
using Application.Utils.ResponseBase;
using MediatR;

namespace Application.ArticulatorApplication.Commands
{
    public class CreateArticulatorCommand : IRequest<Response>
    {

        public CreateArticulatorCommand(CreateArticulatorDto createArticulatorDto)
        {
            CreateArticulatorDto = createArticulatorDto;
        }

        public CreateArticulatorDto CreateArticulatorDto { get; set; }
    }

}
