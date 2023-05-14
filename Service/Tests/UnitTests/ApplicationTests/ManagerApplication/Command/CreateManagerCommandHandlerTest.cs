using Application.ManagerApplication.Commands;
using Application.ManagerApplication.Commands.Handlers;
using Application.ManagerApplication.Dtos;
using Application.Utils;
using Domain.ManagerDomain.Entities;
using Domain.ManagerDomain.Ports;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using static Application.Utils.ResponseBase.Response;

namespace UnitTests.ApplicationTests.ManagerApplication.Command
{
    public class CreateManagerCommandHandlerTest
    {
        private CreateManagerCommandHandler GetCommandMock(
            Mock<IManagerRepository> managerRepository = null)
        {
            var _managerRepository = managerRepository ?? new Mock<IManagerRepository>();

            var commandHandler = new CreateManagerCommandHandler(_managerRepository.Object);

            return commandHandler;
        }

        [Test]
        public void Handler_WhenManagerNoHasRequiredInformation_ShouldReturnBadRequest()
        {
            var managerDto = new CreateManagerDto
            {
                Password = "password",
                Email = "test@gami.com",
                FullName = ""
            };

            var request = new CreateManagerCommand(managerDto);

            var handler = GetCommandMock();

            var response = handler.Handle(request, CancellationToken.None);

            var result = response.Result.Value.Should().BeOfType<BadRequest>().Which;
            _ = result.Message.Should().Be("Manager no has required information");
            _ = result.ErrorCodes.Should().Be(ErrorCodes.MANAGER_MISSING_REQUIRED_INFORMATION);
        }

        [Test]
        public void Handler_WhenUserPasswordLengthIsInvalid_ShouldReturnBadRequest()
        {
            var managerDto = new CreateManagerDto
            {
                Password = "pass",
                Email = "test@gami.com",
                FullName = ""
            };

            var request = new CreateManagerCommand(managerDto);

            var handler = GetCommandMock();

            var response = handler.Handle(request, CancellationToken.None);

            var result = response.Result.Value.Should().BeOfType<BadRequest>().Which;
            _ = result.Message.Should().Be("User password length is invalid");
            _ = result.ErrorCodes.Should().Be(ErrorCodes.USER_LENGTH_IS_INVALID);
        }

        [Test]
        public void Handler_WhenManagerCouldNotBeStored_ShouldReturnInternalServerError()
        {
            var managerDto = new CreateManagerDto
            {
                Password = "password",
                Email = "test@gami.com",
                FullName = "Teste da silva",
                UserName = "username",
                PhoneNumber = "1234567890"
            };

            var request = new CreateManagerCommand(managerDto);

            var managerRepository = new Mock<IManagerRepository>();
            managerRepository.Setup(x => x.Create(It.IsAny<Manager>())).Throws(new Exception());

            var handler = GetCommandMock(managerRepository);

            var response = handler.Handle(request, CancellationToken.None);

            var result = response.Result.Value.Should().BeOfType<InternalServerError>().Which;
            _ = result.Message.Should().Be("Manager Could not be storage");
            _ = result.ErrorCodes.Should().Be(ErrorCodes.MANAGER_COULD_NOT_BE_STORAGE);
        }

        [Test]
        public void Handler_WhenManagerIsValid_ShouldReturnSuccess()
        {
            var managerDto = new CreateManagerDto
            {
                Password = "password",
                Email = "test@gami.com",
                FullName = "Teste da silva",
                UserName = "username",
                PhoneNumber = "1234567890"
            };

            var request = new CreateManagerCommand(managerDto);

            var handler = GetCommandMock();

            var response = handler.Handle(request, CancellationToken.None);

            var result = response.Result.Value.Should().BeOfType<Success>().Which;
        }
    }
}
