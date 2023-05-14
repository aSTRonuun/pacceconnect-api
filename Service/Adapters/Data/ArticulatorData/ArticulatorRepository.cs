using Domain.ArticulatorDomain.Entities;
using Domain.ArticulatorDomain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.ArticulatorData
{
    public class ArticulatorRepository : IArticulatorRepository
    {
        private readonly PACCEConnectDbContext _context;

        public ArticulatorRepository(PACCEConnectDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Articulator articulator)
        {
            _context.Articulators.Add(articulator);
            return await _context.SaveChangesAsync();
        }

        public async Task<Articulator?> GetById(int id)
        {
            return await _context.Articulators.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> Update(Articulator articulator)
        {
            _context.Articulators.Update(articulator);
            return await _context.SaveChangesAsync();
        }
    }
}
