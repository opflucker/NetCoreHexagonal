using NetCoreHexagonal.Domain.Core.Courses;

namespace NetCoreHexagonal.Application.Ports.In.Dtos
{
    public sealed record class CourseDto(string Name);

    public static class CourseExtensions
    {
        public static CourseDto ToDto(this Course course) => new CourseDto(course.Name.Name);
    }
}
