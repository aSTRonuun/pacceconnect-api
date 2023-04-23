using Domain.ManagerDomain.Entities;

namespace Domain.ManagerDomain.Ports
{
    public interface IManagerRepository
    {
        public Task<int> Create(Manager manager);
        public Task<int> Update(Manager manager);
    }
}
