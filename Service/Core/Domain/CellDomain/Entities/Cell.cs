using Domain.ArticulatorDomain.Entities;
using Domain.CellDomain.Enuns;
using Domain.CellDomain.Exceptions;
using Domain.CellDomain.Ports;
using System.ComponentModel.DataAnnotations;
using Action = Domain.CellDomain.Enuns.Action;

namespace Domain.CellDomain.Entities
{
    public class Cell
    {
        public Cell()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            Status = StatusCell.Created;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public StatusCell Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int ArticulatorId { get; set; }
        public Articulator? Articulator { get; set; }
        public CellPlan? CellPlan { get; set; }

        private void ValidateStateCell()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new MissingRequiredInformationException();
            }

            if (ArticulatorId == 0)
            {
                throw new MissingArticulatorEntityRequiredInformationException();
            }
        }

        public bool IsValidate()
        {
            ValidateStateCell();
            CellPlan.IsValidate();
            return true;
        }

        public async Task Save(ICellRepository cellRepository)
        {
            IsValidate();

            if (Id == 0)
            {
                await Create(cellRepository); return;
            }
            await Update(cellRepository); return;
        }

        private async Task Create(ICellRepository cellRepository)
        {
            await cellRepository.Create(this);
        }

        private async Task Update(ICellRepository cellRepository)
        {
            await cellRepository.Update(this);
        }

        public void ChangeState(Action action)
        {
            Status = (Status, action) switch
            {
                (StatusCell.Created, Action.Submit) => StatusCell.Submeted,
                (StatusCell.Submeted, Action.Review) => StatusCell.Reviewed,
                (StatusCell.Submeted, Action.Approve) => StatusCell.Active,
                (StatusCell.Reviewed, Action.Correct) => StatusCell.Corrected,
                (StatusCell.Corrected, Action.Submit) => StatusCell.Submeted,
                (StatusCell.Active, Action.Close) => StatusCell.Closed,
                (StatusCell.Active, Action.Cancel) => StatusCell.Canceled,
                (StatusCell.Active, Action.Submit) => StatusCell.Submeted,
                (StatusCell.Closed, Action.Reopen) => StatusCell.Active,
            };
        }
    }
}
