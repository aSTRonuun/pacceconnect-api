using Application.ArticulatorApplication.Queries.Handlers;
using Application.ArticulatorApplication.Queries;
using Application.Utils;
using Domain.ArticulatorDomain.Entities;
using Domain.ArticulatorDomain.Enuns;
using Domain.ArticulatorDomain.Ports;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using static Application.Utils.ResponseBase.Response;
using Domain.ArticulatorDomain.ValueObjects;

namespace UnitTests.ApplicationTests.ArticulatorApplication.Query
{
    public class GetArticulatorByIdQueryHandlerTest
    {
        private GetArticulatorByIdQueyHandler GetQueryMock(
            Mock<IArticulatorRepository> articulatorRepository = null)
        {
            var _articulatorRepository = articulatorRepository ?? new Mock<IArticulatorRepository>();

            var queryHandler = new GetArticulatorByIdQueyHandler(_articulatorRepository.Object);

            return queryHandler;
        }

        [Test]
        public void Handler_WhenArticulatorNotFound_ShouldReturnBadRequest()
        {
            var request = new GetArticulatorByIdQuery(1234);

            var handler = GetQueryMock();

            var response = handler.Handle(request, CancellationToken.None);

            var result = response.Result.Value.Should().BeOfType<BadRequest>().Which;
            _ = result.Message.Should().Be("Articulator not found");
            _ = result.ErrorCodes.Should().Be(ErrorCodes.ARTICULATOR_NOT_FOUND);
        }

        [Test]
        public void Handler_ArticulatorCouldNoBeFounded_ShouldBeReturnInternalServerError()
        {
            var request = new GetArticulatorByIdQuery(1234);

            var ArticulatorRepository = new Mock<IArticulatorRepository>();
            ArticulatorRepository.Setup(x => x.GetById(It.IsAny<int>())).Throws(new Exception());

            var handler = GetQueryMock(ArticulatorRepository);

            var response = handler.Handle(request, CancellationToken.None);

            var result = response.Result.Value.Should().BeOfType<InternalServerError>().Which;
            _ = result.Message.Should().Be("Articulator could not be found");
            _ = result.ErrorCodes.Should().Be(ErrorCodes.ARTICULATOR_COULD_NOT_BE_FOUND);
        }

        [Test]
        public void Handler_ArticulatorIsValid_ShouldBeReturnSuccess()
        {
            var request = new GetArticulatorByIdQuery(1234);

            var founded = new Articulator()
            {
                Id = 1234,
                UserName = "Test",
                Name = "Test",
                Email = "Test@gmail.com",
                SurName = "Test",
                StudentId = new StudentId
                {
                    Course = Course.SoftwareEngineering,
                    Matriculation = 1233223
                }
            };

            var ArticulatorRepository = new Mock<IArticulatorRepository>();
            ArticulatorRepository.Setup(x => x.GetById(1234)).ReturnsAsync(founded);

            var handler = GetQueryMock(ArticulatorRepository);

            var response = handler.Handle(request, CancellationToken.None);

            var result = response.Result.Value.Should().BeOfType<Success>().Which;
            _ = result.Dto.Should().NotBeNull();
        }
    }
}
