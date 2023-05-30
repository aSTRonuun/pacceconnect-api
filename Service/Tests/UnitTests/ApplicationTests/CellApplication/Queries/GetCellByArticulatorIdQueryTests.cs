using Application.CellApplication.Dtos;
using Application.CellApplication.Queries;
using Application.CellApplication.Queries.Handler;
using Application.Utils;
using Domain.CellDomain.Entities;
using Domain.CellDomain.Ports;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using static Application.Utils.ResponseBase.Response;

namespace UnitTests.ApplicationTests.CellApplication.Queries
{
    public class GetCellByArticulatorIdQueryTests
    {
        private Mock<ICellRepository> _cellRepository;
        private GetCellByUserIdQueryHandler _query;

        [SetUp]
        public void Setup()
        {
            _cellRepository = new Mock<ICellRepository>();
            _query = new GetCellByUserIdQueryHandler(_cellRepository.Object);
        }

        [Test]
        public void Handle_WhenNotExistsCellWithArticitulatorIs_ShouldBeReturnBadRquest()
        {
            // Arrange
            var requestCommand = new GetCellByUserIdQuery(1);

            // Action
            var response = _query.Handle(requestCommand, CancellationToken.None);

            // Assert
            var result = response.Result.Value.Should().BeOfType<BadRequest>().Which;
            _ = result.Message.Should().Be("Cell not found");
            _ = result.ErrorCodes.Should().Be(ErrorCodes.CELL_NOT_FOUND);
        }

        [Test]
        public void Handle_WhenNotItIsPossibleFound_ShouldBeReturnInternalServerError()
        {
            // Arrange
            var requestCommand = new GetCellByUserIdQuery(1);

            _cellRepository.Setup(x => x.GetCellByArticulatorId(It.IsAny<int>()))
                .ThrowsAsync(new Exception());

            // Action
            var response = _query.Handle(requestCommand, CancellationToken.None);

            // Assert
            var result = response.Result.Value.Should().BeOfType<InternalServerError>().Which;
            _ = result.Message.Should().Be("Cell could not be founded");
            _ = result.ErrorCodes.Should().Be(ErrorCodes.CELL_COULD_NOT_BE_FOUND);
        }

        [Test]
        public void Handle_WhenArticulatorIdIsValid_ShouldBeReturnSuccess()
        {
            // Arrange
            var requestCommand = new GetCellByUserIdQuery(1);
            var cellFounded = new Cell()
            {
                Id = 1,
                Name = "Test",
                ArticulatorId = 1,
                CellPlan = new CellPlan()
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
            };

            _cellRepository.Setup(x => x.GetCellByArticulatorId(It.IsAny<int>()))
                .ReturnsAsync(cellFounded);

            // Action
            var response = _query.Handle(requestCommand, CancellationToken.None);

            // Assert
            var result = response.Result.Value.Should().BeOfType<Success>().Which;
            _ = result.Dto.Should().NotBeNull();
        }
    }
}
