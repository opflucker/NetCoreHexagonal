using NetCoreHexagonal.Application.Ports.In;
using NetCoreHexagonal.Application.Ports.In.Dtos;
using NetCoreHexagonal.Application.Services.Context;
using NetCoreHexagonal.Domain.Core.Courses;
using NetCoreHexagonal.Domain.Core.Students;

namespace NetCoreHexagonal.Application.Services
{
    internal sealed class SchoolService : ISchoolService
    {
        private readonly ISchoolContextWithEvents context;

        public SchoolService(ISchoolContextWithEvents context)
        {
            this.context = context;
        }

        public async Task<IReadOnlyList<CourseDto>> GetAllCourses()
        {
            var courses = await context.School.Courses.GetAllAsync();
            return courses.Select(c => c.ToDto()).ToList();
        }

        public async Task<IReadOnlyList<StudentDto>> GetAllStudents()
        {
            var students = await context.School.Students.GetAllAsync();
            return students.Select(s => s.ToDto()).ToList();
        }

        public async Task<CourseDto> RegisterCourse(RegisterCourseDto dto)
        {
            var course = dto.ToCourse();
            context.School.Courses.Register(course);
            await context.SaveChangesAndDispatchEventsAsync();
            return course.ToDto();
        }

        public async Task<StudentDto?> RegisterStudent(RegisterStudentDto dto)
        {
            var student = await dto.ToStudent(context.School.Courses);
            if (student == null)
                return null;

            context.School.Students.Register(student);
            await context.SaveChangesAndDispatchEventsAsync();
            return student.ToDto();
        }

        public async Task EnrollStudent(EnrollStudentDto dto)
        {
            var course = await context.School.Courses.GetByNameAsync(dto.CourseName.ToCourseName());
            var student = await context.School.Students.GetByNameAsync(dto.StudentName.ToStudentName());
            if (student == null || course == null)
                return;

            student.EnrollIn(course);
            await context.SaveChangesAndDispatchEventsAsync();
        }
    }
}
