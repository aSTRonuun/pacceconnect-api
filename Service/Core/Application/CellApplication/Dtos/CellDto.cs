using Application.ArticulatorApplication.Dtos;
using Application.Utils.IDtoBase;
using Domain.CellDomain.Entities;
using Domain.CellDomain.Enuns;

namespace Application.CellApplication.Dtos
{
    public class CellDto : IDto
    {
        public CellDto() 
        {
            Status = StatusCell.Created;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public StatusCell? Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int ArticulatorId { get; set; }
        public ArticulatorDto? Articulator { get; set; }
        public CellPlanDto? Plan { get; set; }

        public static Cell MapToEntity(CellDto cellDto)
        {
            return new Cell
            {
                Id = cellDto.Id,
                Name = cellDto.Name,
                CreatedAt = cellDto.CreatedAt,
                UpdatedAt = cellDto.UpdatedAt,
                ArticulatorId = cellDto.ArticulatorId,
                Articulator = cellDto.Articulator == null ? null : ArticulatorDto.MapToEntity(cellDto.Articulator),
                CellPlan = cellDto.Plan == null ? null : CellPlanDto.MapToEntity(cellDto.Plan)
            };
        }

        public static CellDto MapToDto(Cell cell)
        {
            return new CellDto
            {
                Id = cell.Id,
                Name = cell.Name,
                Status = cell.Status,
                CreatedAt = cell.CreatedAt,
                UpdatedAt = cell.UpdatedAt,
                ArticulatorId = cell.ArticulatorId,
                Articulator = cell.Articulator == null ? null : ArticulatorDto.MapToDto(cell.Articulator),
                Plan = cell.CellPlan == null ? null : CellPlanDto.MapToDto(cell.CellPlan)
            };
        }
    }
}
