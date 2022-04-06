using NetCoreHexagonal.Application.Ports.Out.Persistence;
using NetCoreHexagonal.Domain.Commons;
using NetCoreHexagonal.Persistence.Design;

namespace NetCoreHexagonal.Persistence.Services
{
    internal sealed class SchoolContext : ISchoolContext
    {
        private readonly SchoolDbContext dbContext;

        public IStudentsRepository Students { get; }
        public ICoursesRepository Courses { get; }

        public SchoolContext(SchoolDbContext dbContext)
        {
            this.dbContext = dbContext;
            Students = new StudentsRepository(dbContext);
            Courses = new CoursesRepository(dbContext.Courses);
        }

        public async Task<IReadOnlyList<RootAggregate>> SaveChangesAsync()
        {
            var savedAggregates = dbContext.ChangeTracker.Entries()
                .Where(e => e.Entity is RootAggregate)
                .Select(e => e.Entity as RootAggregate)
                .ToList();

            await dbContext.SaveChangesAsync();

            return savedAggregates!;
        }
    }
}
