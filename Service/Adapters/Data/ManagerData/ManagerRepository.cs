using Domain.ManagerDomain.Entities;
using Domain.ManagerDomain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.ManagerData
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly PACCEConnectDbContext _context;

        public ManagerRepository(PACCEConnectDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Manager manager)
        {
            _context.Managers.Add(manager);
            return await _context.SaveChangesAsync();
        }

        public async Task<Manager?> GetById(int id)
        {
            return await _context.Managers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> Update(Manager manager)
        {
            _context.Managers.Update(manager);
            return await _context.SaveChangesAsync();
        }
    }
}
