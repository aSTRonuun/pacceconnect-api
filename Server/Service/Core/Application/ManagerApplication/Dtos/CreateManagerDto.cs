using Application.Utils.IDtoBase;
using Domain.ManagerDomain.Entities;

namespace Application.ManagerApplication.Dtos
{
    public class CreateManagerDto : IDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public static Manager MapToEntity(CreateManagerDto managerDto)
        {
            return new Manager
            {
                Id = managerDto.Id,
                FullName = managerDto.FullName,
                Phone = managerDto.PhoneNumber,
                UserName = managerDto.UserName,
                Email = managerDto.Email
            };
        }

    }
}
