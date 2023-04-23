using Domain.ArticulatorDomain.Enuns;

namespace Domain.ArticulatorDomain.ValueObjects
{
    public class StudentId
    {
        public int Matriculation { get; set; }
        public Course Course { get; set; }
    }
}
