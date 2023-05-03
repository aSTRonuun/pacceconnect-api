using Application.ArticulatorApplication.Dtos;
using Application.ManagerApplication.Dtos;
using Application.UserApplication.Dtos;
using Application.Utils.ResponseBase;
using MediatR;

namespace Application.ManagerApplication.Commands
{
    public class CreateManagerCommand : IRequest<Response>
    {
        public CreateManagerCommand(Dtos.CreateManagerDto createManagerDto)
        {
            CreateManagerDto = createManagerDto;
        }

        public Dtos.CreateManagerDto CreateManagerDto { get; set; }
    }

}
