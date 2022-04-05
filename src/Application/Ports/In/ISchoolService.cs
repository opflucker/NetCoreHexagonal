using NetCoreHexagonal.Application.Ports.In.Dtos;

namespace NetCoreHexagonal.Application.Ports.In
{
    public interface ISchoolService
    {
        Task<IReadOnlyList<CourseDto>> GetAllCourses();
        Task<IReadOnlyList<StudentDto>> GetAllStudents();
        Task<CourseDto> RegisterCourse(RegisterCourseDto dto);
        Task<StudentDto?> RegisterStudent(RegisterStudentDto dto);
        Task EnrollStudent(EnrollStudentDto dto);
    }
}