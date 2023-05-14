using Application.Utils.IDtoBase;
using Domain.ManagerDomain.Entities;
using Domain.ManagerDomain.Enuns;

namespace Application.ManagerApplication.Dtos
{
    public class ManagerDto : IDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public Status AccountStatus { get; set; }
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

        public static ManagerDto MapToDto(Manager manager)
        {
            return new ManagerDto
            {
                Id = manager.Id,
                FullName = manager.FullName,
                PhoneNumber = manager.Phone,
                AccountStatus = manager.Status,
                Email = manager.Email,
                UserName = manager.UserName,
            };
        }   
    }
}
