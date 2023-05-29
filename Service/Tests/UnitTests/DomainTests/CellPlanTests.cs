using Domain.CellDomain.Entities;
using Domain.CellDomain.Exceptions;
using NUnit.Framework;

namespace UnitTests.DomainTests
{
    public class CellPlanTests
    {
        [Test]
        public void IsValidate_WhenCellPlanNoHasRequiredInformation_ShouldThrowException()
        {
            var cellPlan = new CellPlan()
            {
                Title = "Title",
                Local = "",
            };

            Assert.Throws<MissingRequiredInformationException>(() => cellPlan.IsValidate());
        }

        [Test]
        public void IsValidate_WhenCellPlanHasRequiredInformation_ShouldReturnTrue()
        {
            var cellPlan = new CellPlan()
            {
                Title = "Title",
                Local = "Local",
                Mode = "Mode",
                Synopsis = "Synopsis",
                Justification = "Justification",
                TargetAudience = "TarguetAudience",
                Activities = "Activities",
                Tools = "Tools",
                ResultIndicators = "ResultIndicators",
                MeansOfVerification = "MeansOfVerfication"
            };

            Assert.True(cellPlan.IsValidate());
        }

        [Test]
        public void PlanIsCompleted_WhenHasOneComment_ShouldReturnFalse()
        {
            var cellPlan = new CellPlan()
            {
                Title = "Title",
                TitleComment = "Invalid",
                Local = "Local",
                Mode = "Mode",
                Synopsis = "Synopsis",
                Justification = "Justification",
                TargetAudience = "TarguetAudience",
                Activities = "Activities",
                Tools = "Tools",
                ResultIndicators = "ResultIndicators",
                MeansOfVerification = "MeansOfVerfication"
            };

            Assert.True(cellPlan.IsValidate());
            Assert.IsFalse(cellPlan.PlanIsCompleted());
        }

        [Test]
        public void PlanIsCompleted_WhenNoHasAnyComment_ShouldReturnTrue()
        {
            var cellPlan = new CellPlan()
            {
                Title = "Title",
                Local = "Local",
                Mode = "Mode",
                Synopsis = "Synopsis",
                Justification = "Justification",
                TargetAudience = "TarguetAudience",
                Activities = "Activities",
                Tools = "Tools",
                ResultIndicators = "ResultIndicators",
                MeansOfVerification = "MeansOfVerfication",
            };

            Assert.True(cellPlan.IsValidate());
            Assert.IsTrue(cellPlan.PlanIsCompleted());
        }
    }
}
