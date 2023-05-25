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
        public void PlanIsCompleted_WhenHasOneValidateFalse_ShouldReturnFalse()
        {
            var cellPlan = new CellPlan()
            {
                Title = "Title",
                TitleIsValid = false,
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
        public void PlanIsCompleted_WhenHasAllValidateAreTrue_ShouldReturnTrue()
        {
            var cellPlan = new CellPlan()
            {
                Title = "Title",
                TitleIsValid = true,
                Local = "Local",
                LocalIsValid = true,
                Mode = "Mode",
                ModeIsValid = true,
                Synopsis = "Synopsis",
                SynopsisIsValid = true,
                Justification = "Justification",
                JustificationIsValid = true,
                TargetAudience = "TarguetAudience",
                TargetAudienceIsValid = true,
                Activities = "Activities",
                ActivitiesIsValid = true,
                Tools = "Tools",
                ToolsIsValid = true,
                ResultIndicators = "ResultIndicators",
                ResultIndicatorsIsValid = true,
                MeansOfVerification = "MeansOfVerfication",
                MeansOfVerificationIsValid = true,
                DateIsValid = true,
                DurationIsValid = true,
            };

            Assert.True(cellPlan.IsValidate());
            Assert.IsTrue(cellPlan.PlanIsCompleted());
        }
    }
}
