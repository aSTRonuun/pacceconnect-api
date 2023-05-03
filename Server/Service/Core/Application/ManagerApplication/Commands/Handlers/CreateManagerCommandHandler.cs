﻿using Application.Utils;
using Application.Utils.ResponseBase;
using MediatR;
using static Application.Utils.ResponseBase.Response;
using Domain.ManagerDomain.Ports;
using Application.ManagerApplication.Dtos;

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
            catch (Exception)
            {
                return new BadRequest("Manager Could not be storage", ErrorCodes.ARTICULATOR_COULD_NOT_BE_STORAGE);
            }
        }
    }
}
