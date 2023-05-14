using Application.ArticulatorApplication.Queries.Handlers;
using Application.ManagerApplication.Commands.Handlers;
using Application.ManagerApplication.Queries;
using Application.ManagerApplication.Queries.Handlers;
using Application.Utils;
using Domain.ManagerDomain.Entities;
using Domain.ManagerDomain.Enuns;
using Domain.ManagerDomain.Ports;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using static Application.Utils.ResponseBase.Response;

namespace UnitTests.ApplicationTests.ManagerApplication.Query
{
    public class GetManagerByIdQueryHandlerTest
    {
        private GetManagerByIdQueryHandler GetQueryMock(
            Mock<IManagerRepository> managerRepository = null)
        {
            var _managerRepository = managerRepository ?? new Mock<IManagerRepository>();

            var commandHandler = new GetManagerByIdQueryHandler(_managerRepository.Object);

            return commandHandler;
        }

        [Test]
        public void Handler_WhenManagerNotFound_ShouldReturnBadRequest()
        {
            var request = new GetManagerByIdQuery(1234);
            
            var handler = GetQueryMock();
            
            var response = handler.Handle(request, CancellationToken.None);

            var result = response.Result.Value.Should().BeOfType<BadRequest>().Which;
            _ = result.Message.Should().Be("Manager not found");
            _ = result.ErrorCodes.Should().Be(ErrorCodes.MANAGER_NOT_FOUND);
        }

        [Test]
        public void Handler_ManagerCouldNoBeFounded_ShouldBeReturnInternalServerError()
        {
            var request = new GetManagerByIdQuery(1234);

            var managerRepository = new Mock<IManagerRepository>();
            managerRepository.Setup(x => x.GetById(It.IsAny<int>())).Throws(new Exception());

            var handler = GetQueryMock(managerRepository);

            var response = handler.Handle(request, CancellationToken.None);

            var result = response.Result.Value.Should().BeOfType<InternalServerError>().Which;
            _ = result.Message.Should().Be("Manager could not be found");
            _ = result.ErrorCodes.Should().Be(ErrorCodes.MANAGER_COULD_NOT_BE_FOUND);
        }

        [Test]
        public void Handler_ManagerIsValid_ShouldBeReturnSuccess()
        {
            var request = new GetManagerByIdQuery(1234);

            var founded = new Manager()
            {
                Id = 1234,
                FullName = "Test",
                Email = "Test@test.com",
                CreatedAt = DateTime.UtcNow,
                Phone = "123443",
                Status = Status.Active,
                UserName = "Test"
            };

            var managerRepository = new Mock<IManagerRepository>();
            managerRepository.Setup(x => x.GetById(1234)).ReturnsAsync(founded);

            var handler = GetQueryMock(managerRepository);

            var response = handler.Handle(request, CancellationToken.None);

            var result = response.Result.Value.Should().BeOfType<Success>().Which;
            _ = result.Dto.Should().NotBeNull();
        }
    }
}
