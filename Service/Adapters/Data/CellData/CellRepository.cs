using Domain.CellDomain.Entities;
using Domain.CellDomain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.CellData
{
    public class CellRepository : ICellRepository
    {
        private readonly PACCEConnectDbContext _context;

        public CellRepository(PACCEConnectDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Cell cell)
        {
            _context.Cells.Add(cell);
            return await _context.SaveChangesAsync();
        }

        public async Task<Cell?> GetCellById(int id)
        {
            return await _context.Cells
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Cell?> GetCellByArticulatorId(int articulatorId)
        {
            return await _context.Cells
                .Include(x => x.CellPlan)
                .Include(x => x.Articulator)
                .FirstOrDefaultAsync(x => x.ArticulatorId == articulatorId);
        }

        public async Task<CellPlan?> GetCellPlanByCellId(int cellId)
        {
            return await _context.CellPlans
                .FirstOrDefaultAsync(x => x.Id == cellId);
        }

        public Task<List<Cell>> GetAllCells()
        {
            return _context.Cells
                .Include(x => x.Articulator)
                .ToListAsync();
        }
    }
}
