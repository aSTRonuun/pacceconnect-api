using Domain.ArticulatorDomain.Ports;
using Domain.ArticulatorDomain.ValueObjects;
using Domain.UserDomain.Entities;
using Domain.UserDomain.Enuns;
using Domain.UserDomain.Exceptions;

namespace Domain.ArticulatorDomain.Entities
{
    public class Articulator : User
    {
        public Articulator()
        {
            Role = Roles.Articulator;
        }

        public string Name { get; set; }
        public string SurName { get; set; }
        public StudentId StudentId { get; set; }
        public string? PhoneNumber { get; set; }

        private void ValidateStateArticulator()
        {
            if (StudentId == null ||
                StudentId.Course.Equals(null) ||
                StudentId.Matriculation == 0)
            {
                throw new InvalidStudentIdException();
            }
            
            if (string.IsNullOrEmpty(Name) ||
                string.IsNullOrEmpty(SurName))
            {
                throw new ArticulatorMissingRequiredInformation();
            }
        }

        public bool IsValidate()
        {
            ValidateStateArticulator();
            ValidateStateUser();
            return true;
        }

        public async Task Save(IArticulatorRepository articulatorRepository)
        {
            IsValidate();

            if (Id == 0)
            {
                await Create(articulatorRepository); return;
            }
            await Update(articulatorRepository); return;
        }

        private async Task Create(IArticulatorRepository articulatorRepository)
        {
            await articulatorRepository.Create(this);
        }

        private async Task Update(IArticulatorRepository articulatorRepository)
        {
            await articulatorRepository.Update(this);
        }
    }
}
