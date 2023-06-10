using Application.CellApplication.Commands.Handlers;
using Application.CellApplication.Commands;
using Application.CellApplication.Dtos;
using Application.Utils;
using Domain.CellDomain.Ports;
using Moq;
using NUnit.Framework;
using static Application.Utils.ResponseBase.Response;
using FluentAssertions;
using Domain.CellDomain.Entities;
using Domain.CellDomain.Enuns;

namespace UnitTests.ApplicationTests.ArticulatorApplication.Command
{
    public class UpdateArticulatorCommandHandlerTests
    {
        private Mock<ICellRepository> _cellRepositoryMock;
        private UpdateCellCommandHandler _updateCellCommandHandler;

        [SetUp]
        public void Setup()
        {
            _cellRepositoryMock = new Mock<ICellRepository>();
            _updateCellCommandHandler = new UpdateCellCommandHandler(_cellRepositoryMock.Object);
        }

        [Test]
        public void Handler_WhenRequestDtoNoHasRequiredInformations_ShouldBeReturnBadRequest()
        {
            // Arrange
            var requestInvalid = new UpdateCellCommand(new CellDto() { Name = "" });

            var cellOld = new Cell()
            {
                Status = StatusCell.Submeted,
            };

            _cellRepositoryMock.Setup(x => x.GetCellById(It.IsAny<int>()))
                .ReturnsAsync(cellOld);

            // Action
            var response = _updateCellCommandHandler.Handle(requestInvalid, CancellationToken.None);

            // Assert
            var result = response.Result.Value.Should().BeOfType<BadRequest>().Which;
            _ = result.Message.Should().Be("Cell no has required information");
            _ = result.ErrorCodes.Should().Be(ErrorCodes.CELL_MISSING_REQUIRED_INFORMATIONS);
        }

        [Test]
        public void Handler_WhenRequestDtoArticulatorInformation_ShouldBeReturnBadRequest()
        {
            // Arrange
            var requestInvalid = new UpdateCellCommand(new CellDto()
                {
                    Name = "Test"
                }
            );

            var cellOld = new Cell()
            {
                Status = StatusCell.Submeted,
            };

            _cellRepositoryMock.Setup(x => x.GetCellById(It.IsAny<int>()))
                .ReturnsAsync(cellOld);

            // Action
            var response = _updateCellCommandHandler.Handle(requestInvalid, CancellationToken.None);

            // Assert
            var result = response.Result.Value.Should().BeOfType<BadRequest>().Which;
            _ = result.Message.Should().Be("Cell no has articulator required information");
            _ = result.ErrorCodes.Should().Be(ErrorCodes.CELL_MISSING_ARTICULATOR_INFORMATION);
        }

        [Test]
        public void Handler_WhenNotIsPossibleCreateCell_ShouldBeReturnInternalServerError()
        {
            // Arrange
            var requestInvalid = new UpdateCellCommand(new CellDto()
            {
                Id = 1,
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

            var cellOld = new Cell()
            {
                Status = StatusCell.Submeted,
            };

            _cellRepositoryMock.Setup(x => x.GetCellById(It.IsAny<int>()))
                .ReturnsAsync(cellOld);

            _cellRepositoryMock.Setup(x => x.Update(It.IsAny<Cell>()))
                .ThrowsAsync(new Exception());

            // Action
            var response = _updateCellCommandHandler.Handle(requestInvalid, CancellationToken.None);

            // Assert
            var result = response.Result.Value.Should().BeOfType<InternalServerError>().Which;
            _ = result.Message.Should().Be("Cell could not be storage");
            _ = result.ErrorCodes.Should().Be(ErrorCodes.CELL_COULD_NOT_BE_STORAGE);
        }

        [Test]
        public void Handler_WhenCellIdNoExists_ShouldBeReturnBadRequest()
        {
            // Arrange
            var requestInvalid = new UpdateCellCommand(new CellDto()
            {
                Id = 1,
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

            _cellRepositoryMock.Setup(x => x.Update(It.IsAny<Cell>()))
                .ThrowsAsync(new Exception());

            // Action
            var response = _updateCellCommandHandler.Handle(requestInvalid, CancellationToken.None);

            // Assert
            var result = response.Result.Value.Should().BeOfType<BadRequest>().Which;
            _ = result.Message.Should().Be("Cell not found");
            _ = result.ErrorCodes.Should().Be(ErrorCodes.CELL_NOT_FOUND);
        }

        [Test]
        public void Handler_WhenRequestDtoIsValid_ShouldBeRerunSuccess()
        {
            // Arrange
            var requestInvalid = new UpdateCellCommand(new CellDto()
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

            var cellOld = new Cell()
            {
                Status = StatusCell.Submeted,
            };

            _cellRepositoryMock.Setup(x => x.GetCellById(It.IsAny<int>()))
                .ReturnsAsync(cellOld);

            _cellRepositoryMock.Setup(x => x.Create(It.IsAny<Cell>()))
                .ReturnsAsync(1);

            // Action
            var response = _updateCellCommandHandler.Handle(requestInvalid, CancellationToken.None);

            // Assert
            var result = response.Result.Value.Should().BeOfType<Success>().Which;
            _ = result.Dto.Should().NotBeNull();
        }
    }
}
