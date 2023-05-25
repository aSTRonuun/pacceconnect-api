using Application.Utils.IDtoBase;
using Domain.ArticulatorDomain.Entities;
using Domain.ArticulatorDomain.Enuns;
using Domain.ArticulatorDomain.ValueObjects;

namespace Application.ArticulatorApplication.Dtos
{
    public class ArticulatorDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Course Course { get; set; }
        public int Matriculation { get; set; }

        public static Articulator MapToEntity(ArticulatorDto articulatorDto)
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
                PhoneNumber = articulatorDto.PhoneNumber
            };
        }

        public static ArticulatorDto MapToDto(Articulator articulator)
        {
            return new ArticulatorDto
            {
                Id = articulator.Id,
                Name = articulator.Name,
                SurName = articulator.SurName,
                UserName = articulator.UserName,
                Email = articulator.Email,
                PhoneNumber = articulator.PhoneNumber,
                Course = articulator.StudentId.Course,
                Matriculation = articulator.StudentId.Matriculation,
            };
        }
    }
}
