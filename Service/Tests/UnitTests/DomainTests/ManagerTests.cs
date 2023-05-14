using Domain.ManagerDomain.Entities;
using Domain.ManagerDomain.Exceptions;
using NUnit.Framework;

namespace UnitTests.DomainTests
{
    public class ManagerTests
    {
        [Test]
        public void ManagerIsValid_WhenPropertiesAreNullOrEmpty_ShouldBeThrowsException()
        {
            var managerTest = new Manager();

            Assert.Throws<ManagerMissingRequiredInformationException>(() => managerTest.IsValidate());
        }

        [Test]
        public void ManagerIsValid_WhenPropertiesAreValids_ShouldBeReturnTrue()
        {
            var managerTest = new Manager()
            {
                Email = "test@test.com",
                FullName = "Test da silva",
                Phone = "0000000",
                UserName = "Test",
            };

            Assert.True(managerTest.IsValidate());
        }
    }
}
