using Application.ArticulatorApplication.Dtos;
using Application.ManagerApplication.Dtos;
using Application.UserApplication.Dtos;
using Application.Utils.ResponseBase;
using MediatR;

namespace Application.ManagerApplication.Commands
{
    public class CreateManagerCommand : IRequest<Response>
    {
        public CreateManagerCommand(CreateManagerDto createManagerDto)
        {
            CreateManagerDto = createManagerDto;
        }

        public CreateManagerDto CreateManagerDto { get; set; }
    }

}
