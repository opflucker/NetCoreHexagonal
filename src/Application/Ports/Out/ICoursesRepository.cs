using NetCoreHexagonal.Domain.Core.Courses;

namespace NetCoreHexagonal.Application.Ports.Out
{
    public interface ICoursesRepository
    {
        Task<IReadOnlyList<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(Guid id);
        Task<Course?> GetByNameAsync(CourseName name);
        void Register(Course course);
    }
}
