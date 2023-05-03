using Domain.ArticulatorDomain.Entities;

namespace Domain.ArticulatorDomain.Ports
{
    public interface IArticulatorRepository
    {
        public Task<int> Create(Articulator articulator);
        public Task<int> Update(Articulator articulator);
    }
}
