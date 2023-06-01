using Domain.CellDomain.Entities;
using Domain.CellDomain.Enuns;
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
                .Include(x => x.CellPlan)
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

        public async Task<IEnumerable<Cell>> GetAllCells()
        {
            return await _context.Cells
                .Include(x => x.Articulator)
                .Where(x => x.Status != StatusCell.Created)
                .ToListAsync();
        }

        public async Task<int> Update(Cell cell)
        {
            _context.Cells.Update(cell);
            return await _context.SaveChangesAsync(); 
        }
    }
}
