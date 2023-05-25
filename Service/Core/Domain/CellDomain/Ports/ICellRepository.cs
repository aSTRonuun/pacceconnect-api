using Domain.CellDomain.Entities;

namespace Domain.CellDomain.Ports
{
    public interface ICellRepository
    {
        public Task<int> Create(Cell cell);
        public Task<Cell?> GetCellById(int Id);
        public Task<Cell?> GetCellByArticulatorId(int articulatorId);
        public Task<CellPlan?> GetCellPlanByCellId(int cellId);
        public Task<List<Cell>> GetAllCells();
    }
}
