using Domain.ManagerDomain.Enuns;
using Domain.ManagerDomain.Exceptions;
using Domain.ManagerDomain.Ports;
using Domain.UserDomain.Entities;
using Domain.UserDomain.Enuns;

namespace Domain.ManagerDomain.Entities
{
    public class Manager : User
    {
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty ;
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public Manager()
        {
            Role = Roles.Manager;
            Status = Status.Active;
            CreatedAt = DateTime.UtcNow;
        }

        private void ValidateStateManager()
        {
            if (string.IsNullOrEmpty(FullName) || 
                string.IsNullOrEmpty(Phone)) 
            {
                throw new ManagerMissingRequiredInformationException();
            }
        }

        public bool IsValidate()
        {
            ValidateStateManager();
            ValidateStateUser();
            return true;
        }

        public async Task Save(IManagerRepository managerRepository)
        {
            IsValidate();

            if (Id == 0)
            {
                await Create(managerRepository); return;
            }
            await Update(managerRepository); return;
        }

        private async Task Create(IManagerRepository managerRepository)
        {
            await managerRepository.Create(this);
        }

        private async Task Update(IManagerRepository managerRepository)
        {
            await managerRepository.Create(this);
        }
    }
}
