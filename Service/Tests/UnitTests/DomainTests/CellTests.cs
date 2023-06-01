using Domain.CellDomain.Entities;
using Domain.CellDomain.Enuns;
using Domain.CellDomain.Exceptions;
using NUnit.Framework;
using Action = Domain.CellDomain.Enuns.Action;

namespace UnitTests.DomainTests
{
    public class CellTests
    {
        private Cell cell { get; set; }

        [SetUp]
        public void Setup()
        {
            cell = new Cell();
        }

        [Test]
        public void IsValidate_WhenCellNoHasRequiredInformation_ShouldBeThrowException() 
        {
            // Arrange
            cell.Name = "";
            

            // Assert
            Assert.Throws<CellMissingRequiredInformationException>(() => cell.IsValidate());
        }

        [Test]
        public void IsValidate_WhenCellNoHasArticulatorIdInformation_ShouldBeThrowException()
        {
            // Arrange
            cell.Name = "Cell 1";
            cell.ArticulatorId = 0;

            // Assert
            Assert.Throws<CellMissingArticulatorEntityRequiredInformationException>(() => cell.IsValidate());
        }

        [Test]
        public void IsValidate_WhenCellIsValid_ShouldBeReturnTrue() 
        {
            // Arrange
            cell.Name = "Cell 1";
            cell.ArticulatorId = 1;
            cell.CellPlan = new CellPlan()
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

            // Act
            var result = cell.IsValidate();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ChangeState_WhenCellIsCreatedAndActionIsSubmite_FinalResultShouldBeSubmitted()
        {
            // Arrange
            var action = Action.Submit;

            // Act
            cell.ChangeState(action);

            // Assert
            Assert.AreEqual(StatusCell.Submeted, cell.Status);
        }

        [Test]
        public void ChangeState_WhenCellIsSubmitedAndActionIsReview_FinalResultShouldBeReviewd() 
        {
            // Arrange
            cell.ChangeState(Action.Submit);

            // Act
            cell.ChangeState(Action.Review);

            // Assert
            Assert.AreEqual(StatusCell.Reviewed, cell.Status);
        }

        [Test]
        public void ChangeState_WhenCellIsReviewdAndActionIsCorrect_FinalResultShouldBeCorrected()
        {
            // Arrange
            cell.ChangeState(Action.Submit);
            cell.ChangeState(Action.Review);

            // Act
            cell.ChangeState(Action.Correct);

            // Assert
            Assert.AreEqual(StatusCell.Corrected, cell.Status);
        }

        [Test]
        public void ChangeState_WhenCellIsCorrectedAndActionIsReview_FinalResultShouldBeReviewd()
        {
            // Arrange
            cell.ChangeState(Action.Submit);
            cell.ChangeState(Action.Review);
            cell.ChangeState(Action.Correct);

            // Act
            cell.ChangeState(Action.Review);

            // Assert
            Assert.AreEqual(StatusCell.Reviewed, cell.Status);
        }

        [Test]
        public void ChangeState_WhenCellIsCorrectedAndActionIsApprove_FinalResultShouldBeActive()
        {
            // Arrange
            cell.ChangeState(Action.Submit);
            cell.ChangeState(Action.Review);
            cell.ChangeState(Action.Correct);

            // Act
            cell.ChangeState(Action.Approve);

            // Assert
            Assert.AreEqual(StatusCell.Active, cell.Status);
        }

        [Test]
        public void ChangeState_WhenCellIsActiveAndActionIsClose_FinalResultShouldBeClosed()
        {
            // Arrange
            cell.ChangeState(Action.Submit);
            cell.ChangeState(Action.Review);
            cell.ChangeState(Action.Correct);
            cell.ChangeState(Action.Approve);

            // Act
            cell.ChangeState(Action.Close);

            // Assert
            Assert.AreEqual(StatusCell.Closed, cell.Status);
        }

        [Test]
        public void ChangeState_WhenCellIsActiveAndActionIsSubmit_FinalResultShouldBeSubmitted()
        {
            // Arrange
            cell.ChangeState(Action.Submit);
            cell.ChangeState(Action.Review);
            cell.ChangeState(Action.Correct);
            cell.ChangeState(Action.Approve);

            // Act
            cell.ChangeState(Action.Submit);

            // Assert
            Assert.AreEqual(StatusCell.Submeted, cell.Status);
        }

        [Test]
        public void ChangeState_WhenCellIsSubmitedAndActionIsApprove_FinalResultShouldBeActive()
        {
            // Arrange
            cell.ChangeState(Action.Submit);
            cell.ChangeState(Action.Review);
            cell.ChangeState(Action.Correct);
            cell.ChangeState(Action.Approve);
            cell.ChangeState(Action.Submit);

            // Act
            cell.ChangeState(Action.Approve);

            // Assert
            Assert.AreEqual(StatusCell.Active, cell.Status);
        }

        [Test]
        public void ChangeState_WhenCellIsActiveAndActionIsReview_FinalResultShouldBeReviewd()
        {
            // Arrange
            cell.ChangeState(Action.Submit);
            cell.ChangeState(Action.Review);
            cell.ChangeState(Action.Correct);
            cell.ChangeState(Action.Approve);

            // Act
            cell.ChangeState(Action.Review);

            // Assert
            Assert.AreEqual(StatusCell.Reviewed, cell.Status);
        }

        [Test]
        public void ChangeState_WhenCellIsClosedAndActionIsReopen_FinalResultShouldBeActive()
        {
            // Arrange
            cell.ChangeState(Action.Submit);
            cell.ChangeState(Action.Review);
            cell.ChangeState(Action.Correct);
            cell.ChangeState(Action.Approve);
            cell.ChangeState(Action.Close);

            // Act
            cell.ChangeState(Action.Reopen);

            // Assert
            Assert.AreEqual(StatusCell.Active, cell.Status);
        }

    }
}
