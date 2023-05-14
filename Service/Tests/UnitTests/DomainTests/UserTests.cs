using Domain.UserDomain.Entities;
using Domain.UserDomain.Enuns;
using Domain.UserDomain.Exceptions;
using NUnit.Framework;

namespace UnitTests.DomainTests
{
    public class UserTests
    {
        [Test]
        public void IsValidateUser_WhenUserInformationAreInvalid_ShouldBeReturnTrue()
        {
            var user = new User();
            Assert.Throws<UserMissingRequiredInformationException>(() => user.IsValidateUser());
        }
        [Test]
        public void IsValidateUser_WhenUserEmailAreInvalid_ShouldBeThrowException()
        {
            var user = new User()
            {
                UserName = "Test",
                Email = "Testemail",
                Role = Roles.Articulator,
            };

            Assert.Throws<UserInvalidEmailException>(() => user.IsValidateUser());
        }
    }
}
