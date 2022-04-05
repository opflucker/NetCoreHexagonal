using NetCoreHexagonal.Domain.Core.Courses;

namespace NetCoreHexagonal.Application.Ports.In.Dtos
{
    public sealed record class RegisterCourseDto(string Name)
    {
        public Course ToCourse() => new(Name.ToCourseName());
    }
}
