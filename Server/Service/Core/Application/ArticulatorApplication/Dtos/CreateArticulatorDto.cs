using Application.Utils.IDtoBase;
using Domain.ArticulatorDomain.Entities;
using Domain.ArticulatorDomain.Enuns;
using Domain.ArticulatorDomain.ValueObjects;

namespace Application.ArticulatorApplication.Dtos
{
    public class CreateArticulatorDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Course Course { get; set; }
        public int Matriculation { get; set; }
        public string? PhoneNumer { get; set; }

        public static Articulator MapToEntity(CreateArticulatorDto articulatorDto)
        {
            return new Articulator
            {
                Id = articulatorDto.Id,
                Name = articulatorDto.Name,
                SurName = articulatorDto.SurName,
                UserName = articulatorDto.UserName,
                Email = articulatorDto.Email,
                StudentId = new StudentId
                {
                    Course = articulatorDto.Course,
                    Matriculation = articulatorDto.Matriculation,
                },
                PhoneNumber = articulatorDto.PhoneNumer
            };
        }
    }
}
