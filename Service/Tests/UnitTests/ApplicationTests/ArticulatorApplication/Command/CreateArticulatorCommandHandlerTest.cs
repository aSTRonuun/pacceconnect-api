using Application.ArticulatorApplication.Commands;
using Application.ArticulatorApplication.Commands.Handlers;
using Application.ArticulatorApplication.Dtos;
using Application.Utils;
using Domain.ArticulatorDomain.Entities;
using Domain.ArticulatorDomain.Enuns;
using Domain.ArticulatorDomain.Ports;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using static Application.Utils.ResponseBase.Response;

namespace UnitTests.ApplicationTests.ArticulatorApplication.Command
{
    public class CreateArticulatorCommandHandlerTest
    {
        private CreateArticulatorCommandHandler GetCommandMock(
            Mock<IArticulatorRepository> articulatorRepository = null)
        {
            var _articulatorRepository = articulatorRepository ?? new Mock<IArticulatorRepository>();

            var commandHandler = new CreateArticulatorCommandHandler(_articulatorRepository.Object);

            return commandHandler;
        }

        [Test]
        public void Handler_WhenArticulatorNoHasRequiredInformation_ShouldReturnBadRequest()
        {
            var ArticulatorDto = new CreateArticulatorDto
            {
                Password = "password",
                Email = "test@gami.com",
                Name = ""
            };

            var request = new CreateArticulatorCommand(ArticulatorDto);

            var handler = GetCommandMock();

            var response = handler.Handle(request, CancellationToken.None);

            var result = response.Result.Value.Should().BeOfType<BadRequest>().Which;
            _ = result.Message.Should().Be("Articulator no has required infomations");
            _ = result.ErrorCodes.Should().Be(ErrorCodes.ARTICULATOR_MISSING_REQUIRED_INFORMATION);
        }

        [Test]
        public void Handler_WhenUserPasswordLengthIsInvalid_ShouldReturnBadRequest()
        {
            var ArticulatorDto = new CreateArticulatorDto
            {
                Password = "pass",
                Email = "test@gami.com",
            };

            var request = new CreateArticulatorCommand(ArticulatorDto);

            var handler = GetCommandMock();

            var response = handler.Handle(request, CancellationToken.None);

            var result = response.Result.Value.Should().BeOfType<BadRequest>().Which;
            _ = result.Message.Should().Be("User password length is invalid");
            _ = result.ErrorCodes.Should().Be(ErrorCodes.USER_LENGTH_IS_INVALID);
        }

        [Test]
        public void Handler_WhenArticulatorCouldNotBeStored_ShouldReturnInternalServerError()
        {
            var ArticulatorDto = new CreateArticulatorDto
            {
                Password = "password",
                Email = "test@gami.com",
                Name = "Teste da silva",
                UserName = "username",
                SurName = "username",
                Matriculation = 128291,
                Course = Course.DigitalDesign
            };

            var request = new CreateArticulatorCommand(ArticulatorDto);

            var ArticulatorRepository = new Mock<IArticulatorRepository>();
            ArticulatorRepository.Setup(x => x.Create(It.IsAny<Articulator>())).Throws(new Exception());

            var handler = GetCommandMock(ArticulatorRepository);

            var response = handler.Handle(request, CancellationToken.None);

            var result = response.Result.Value.Should().BeOfType<InternalServerError>().Which;
            _ = result.Message.Should().Be("Articulator could not be storage");
            _ = result.ErrorCodes.Should().Be(ErrorCodes.ARTICULATOR_COULD_NOT_BE_STORAGE);
        }

        [Test]
        public void Handler_WhenArticulatorIsValid_ShouldReturnSuccess()
        {
            var ArticulatorDto = new CreateArticulatorDto
            {
                Password = "password",
                Email = "test@gami.com",
                Name = "Teste da silva",
                UserName = "username",
                SurName = "username",
                Matriculation = 128291,
                Course = Course.DigitalDesign
            };

            var request = new CreateArticulatorCommand(ArticulatorDto);

            var handler = GetCommandMock();

            var response = handler.Handle(request, CancellationToken.None);

            _ = response.Result.Value.Should().BeOfType<Success>().Which;
        }
    }
}
