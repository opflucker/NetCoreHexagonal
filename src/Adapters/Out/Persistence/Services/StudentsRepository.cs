using Microsoft.EntityFrameworkCore;
using NetCoreHexagonal.Application.Ports.Out.Persistence;
using NetCoreHexagonal.Domain.Core.Students;
using NetCoreHexagonal.Persistence.Design;

namespace NetCoreHexagonal.Persistence.Services
{
    internal sealed class StudentsRepository : IStudentsRepository
    {
        private readonly SchoolDbContext context;

        public StudentsRepository(SchoolDbContext context)
        {
            this.context = context;
        }

        public async Task<IReadOnlyList<Student>> GetAllAsync()
        {
            return await context.Students
                .Include(s => s.FavoriteCourse)
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(Guid id)
        {
            var student = await context.Students.FindAsync(id);

            if (student != null)
            {
                await Task.WhenAll(
                    context.Entry(student)
                        .Reference(s => s.FavoriteCourse)
                        .LoadAsync(),
                    context.Entry(student)
                        .Collection(s => s.Enrollments)
                        .Query()
                        .Include(e => e.Course)
                        .LoadAsync());
            }

            return student;
        }

        public async Task<Student?> GetByNameAsync(StudentName name)
        {
            var student = await context.Students
                .Include(s => s.FavoriteCourse)
                .SingleOrDefaultAsync(s => s.Name == name);

            if (student != null)
            {
                await context.Entry(student)
                    .Collection(s => s.Enrollments)
                    .Query()
                    .Include(e => e.Course)
                    .LoadAsync();
            }

            return student;
        }

        public void Register(Student student)
        {
            context.Students.Add(student);
        }
    }
}
