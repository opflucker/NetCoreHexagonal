using NetCoreHexagonal.Domain.Commons;
using NetCoreHexagonal.Domain.Core.Courses;

namespace NetCoreHexagonal.Domain.Core.Students
{
    public class Enrollment : Entity
    {
        public Student Student { get; }
        public Course Course { get; }

        public Enrollment(Student student, Course course)
        {
            Student = student;
            Course = course;
        }

        #pragma warning disable CS8618
        private Enrollment() { }
        #pragma warning restore CS8618
    }
}