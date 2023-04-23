using Application.Utils.IDtoBase;
using Domain.ArticulatorDomain.Enuns;

namespace Application.ArticulatorApplication.Dtos
{
    public class ArticulatorDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Course Course { get; set; }
        public int Matriculation { get; set; }
    }
}
