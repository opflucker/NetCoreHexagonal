using Microsoft.EntityFrameworkCore;
using NetCoreHexagonal.Application.Ports.Out;
using NetCoreHexagonal.Domain.Core.Courses;

namespace NetCoreHexagonal.Persistence
{
    internal sealed class CoursesRepository : ICoursesRepository
    {
        private readonly DbSet<Course> Courses;

        public CoursesRepository(DbSet<Course> Courses)
        {
            this.Courses = Courses;
        }

        public async Task<IReadOnlyList<Course>> GetAllAsync()
        {
            return await Courses.ToListAsync();
        }

        public async Task<Course?> GetByIdAsync(Guid id)
        {
            return await Courses.FindAsync(id);
        }

        public Task<Course?> GetByNameAsync(CourseName name)
        {
            return Courses.SingleOrDefaultAsync(c => c.Name == name);
        }

        public void Register(Course course)
        {
            Courses.Add(course);
        }
    }
}
