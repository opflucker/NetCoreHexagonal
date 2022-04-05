using NetCoreHexagonal.Domain.Commons;

namespace NetCoreHexagonal.Application.Ports.Out
{
    public interface ISchoolContext
    {
        ICoursesRepository Courses { get; }
        IStudentsRepository Students { get; }

        Task<IReadOnlyList<RootAggregate>> SaveChangesAsync();
    }
}