using Application.CellApplication.Commands;
using Application.CellApplication.Commands.Handlers;
using Application.CellApplication.Dtos;
using Application.Utils;
using Domain.CellDomain.Entities;
using Domain.CellDomain.Ports;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using static Application.Utils.ResponseBase.Response;

namespace UnitTests.ApplicationTests.CellApplication.Commands
{
    public class CreateCellCommandHandlerTests
    {
        private Mock<ICellRepository> _cellRepositoryMock;
        private CreateCellCommandHandler _createCellCommandHandler;

        [SetUp]
        public void Setup()
        {
            _cellRepositoryMock = new Mock<ICellRepository>();
            _createCellCommandHandler = new CreateCellCommandHandler(_cellRepositoryMock.Object);
        }

        [Test]
        public void Handler_WhenRequestDtoNoHasRequiredInformations_ShouldBeReturnBadRequest()
        {
            // Arrange
            var requestInvalid = new CreateCellCommand(new CellDto() { Name = "" });

            // Action
            var response = _createCellCommandHandler.Handle(requestInvalid, CancellationToken.None);

            // Assert
            var result = response.Result.Value.Should().BeOfType<BadRequest>().Which;
            _ = result.Message.Should().Be("Cell no has required information");
            _ = result.ErrorCodes.Should().Be(ErrorCodes.CELL_MISSING_REQUIRED_INFORMATIONS);
        }

        [Test]
        public void Handler_WhenRequestDtoArticulatorInformation_ShouldBeReturnBadRequest()
        {
            // Arrange
            var requestInvalid = new CreateCellCommand(new CellDto()
            {
                Name = "Test"
            }
            );

            // Action
            var response = _createCellCommandHandler.Handle(requestInvalid, CancellationToken.None);

            // Assert
            var result = response.Result.Value.Should().BeOfType<BadRequest>().Which;
            _ = result.Message.Should().Be("Cell no has articulator required information");
            _ = result.ErrorCodes.Should().Be(ErrorCodes.CELL_MISSING_ARTICULATOR_INFORMATION);
        }

        [Test]
        public void Handler_WhenNotIsPossibleCreateCell_ShouldBeReturnInternalServerError()
        {
            // Arrange
            var requestInvalid = new CreateCellCommand(new CellDto()
            {
                Name = "Test",
                ArticulatorId = 1,
                Plan = new CellPlanDto()
                {
                    Title = "Test",
                    Activities = "Test",
                    Justification = "Test",
                    MeansOfVerification = "Test",
                    Local = "Test",
                    Mode = "Test",
                    Synopsis = "Test",
                    Tools = "Test",
                    ResultIndicators = "Test",
                    TargetAudience = "Test",
                }
            }
            );

            _cellRepositoryMock.Setup(x => x.Create(It.IsAny<Cell>()))
                .ThrowsAsync(new Exception());

            // Action
            var response = _createCellCommandHandler.Handle(requestInvalid, CancellationToken.None);

            // Assert
            var result = response.Result.Value.Should().BeOfType<InternalServerError>().Which;
            _ = result.Message.Should().Be("Cell could not be storage");
            _ = result.ErrorCodes.Should().Be(ErrorCodes.CELL_COULD_NOT_BE_STORAGE);
        }

        [Test]
        public void Handler_WhenRequestDtoIsValid_ShouldBeRerunSuccess()
        {
            // Arrange
            var requestInvalid = new CreateCellCommand(new CellDto()
            {
                Name = "Test",
                ArticulatorId = 1,
                Plan = new CellPlanDto()
                {
                    Title = "Test",
                    Activities = "Test",
                    Justification = "Test",
                    MeansOfVerification = "Test",
                    Local = "Test",
                    Mode = "Test",
                    Synopsis = "Test",
                    Tools = "Test",
                    ResultIndicators = "Test",
                    TargetAudience = "Test",
                }
            }
            );

            _cellRepositoryMock.Setup(x => x.Create(It.IsAny<Cell>()))
                .ReturnsAsync(1);

            // Action
            var response = _createCellCommandHandler.Handle(requestInvalid, CancellationToken.None);

            // Assert
            var result = response.Result.Value.Should().BeOfType<Success>().Which;
            _ = result.Dto.Should().NotBeNull();
        }
    }
}
