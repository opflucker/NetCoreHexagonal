using NetCoreHexagonal.Domain.Core.Students;

namespace NetCoreHexagonal.Application.Ports.In.Dtos
{
    public sealed record class EnrollmentDto(CourseDto Course);

    public sealed record class StudentDto(string Name, CourseDto FavoriteCourse, IReadOnlyList<EnrollmentDto> Enrollments);

    public static class StudentExtensions
    {
        public static StudentDto ToDto(this Student student) => new(
            student.Name.Name,
            new(student.FavoriteCourse.Name.Name),
            student.Enrollments.Select(e => new EnrollmentDto(e.Course.ToDto())).ToList());
    }
}
