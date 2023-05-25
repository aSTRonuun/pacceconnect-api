using Domain.ArticulatorDomain.Entities;
using Domain.CellDomain.Enuns;
using Domain.CellDomain.Exceptions;
using Domain.CellDomain.Ports;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
            if (CellPlan != null)
            {
                CellPlan.IsValidate();
            } 
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
            throw new NotImplementedException();
        }
    }
}
