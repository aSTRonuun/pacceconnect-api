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
    public class GetAllCellsQueryHandlerTests
    {
        private Mock<ICellRepository> _cellRepository;
        private GetAllCellsQueryHandler _query;

        [SetUp]
        public void Setup()
        {
            _cellRepository = new Mock<ICellRepository>();
            _query = new GetAllCellsQueryHandler(_cellRepository.Object);
        }

        [Test]
        public void Handler_WhenResultListIsEmpty_ShouldBeReturnSucess()
        {
            // Arrange
            var request = new GetAllCellsQuery();
            var resultList = new List();

            // Assert
            var response = _query.Handle(request, CancellationToken.None);

            // Assert
            var result = response.Result.Value.Should().BeOfType<Success>().Which;
            _ = result.Should().NotBeNull();
            _ = result.Dtos.Should().NotBeNull();
            _ = result.Dtos.Count().Should().Be(0);
        }

        [Test]
        public void Handler_WhenResultListNotIsEmpty_ShouldBeReturnSucess()
        {
            // Arrange
            var request = new GetAllCellsQuery();
            var resultList = new List<Cell>()
            {
                new Cell()
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
                },
                new Cell()
                {
                    Id = 2,
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
                },
            };

            _cellRepository.Setup(x => x.GetAllCells())
                .ReturnsAsync(resultList);

            // Action
            var response = _query.Handle(request, CancellationToken.None);

            // Assert
            var result = response.Result.Value.Should().BeOfType<Success>().Which;
            _ = result.Should().NotBeNull();
            _ = result.Dtos.Should().NotBeNull();
            _ = result.Dtos.Count().Should().Be(2);
        }
    }
}
