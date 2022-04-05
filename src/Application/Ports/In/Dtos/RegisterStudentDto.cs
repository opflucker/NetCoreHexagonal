using NetCoreHexagonal.Application.Ports.Out;
using NetCoreHexagonal.Domain.Core.Courses;
using NetCoreHexagonal.Domain.Core.Students;

namespace NetCoreHexagonal.Application.Ports.In.Dtos
{
    public sealed record class RegisterStudentDto(string Name, string FavoriteCourseName)
    {
        public async Task<Student?> ToStudent(ICoursesRepository coursesRepository)
        {
            var favoriteCourse = await coursesRepository.GetByNameAsync(FavoriteCourseName.ToCourseName());
            if (favoriteCourse == null)
                return null;
            return new(Name.ToStudentName(), favoriteCourse);
        }
    }
}
