using Domain.ArticulatorDomain.Entities;
using Domain.ArticulatorDomain.Enuns;
using Domain.ArticulatorDomain.ValueObjects;
using Domain.UserDomain.Enuns;
using Domain.UserDomain.Exceptions;
using NUnit.Framework;

namespace UnitTests.DomainTests
{
    public class ArticulatorTests
    {
        [Test]
        public void ArticulatorIsValidate_WhenNameOrSurNameAreNullOrEmpty_ShouldBeThorwsException()
        {
            var articulator = new Articulator();

            Assert.Throws<ArticulatorMissingRequiredInformation>(() => articulator.IsValidate());
        }

        [Test]
        public void ArticulatorIsValidate_WhenStudentIdIsNullOrEmpty_ShouldBeThorwsException()
        {
            var articulator = new Articulator()
            {
                Name = "Test",
                SurName = "da Silva"
            };

            Assert.Throws<InvalidStudentIdException>(() => articulator.IsValidate());
        }

        [Test]
        public void ArticulatorIsValidate_WhenArticulatorInformationIsValid_ShouldBeReturnTrue()
        {
            var articulator = new Articulator()
            {
                Name = "Test",
                SurName = "da Silva",
                StudentId = new StudentId()
                {
                    Course = Course.SoftwareEngineering,
                    Matriculation = 123456
                },
                UserName = "Test",
                Email = "Testemail@gmail.com",
                Role = Roles.Articulator,
            };

            Assert.True(articulator.IsValidate());
        }
    }
}
