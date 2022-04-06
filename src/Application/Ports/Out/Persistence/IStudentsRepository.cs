using NetCoreHexagonal.Domain.Core.Students;

namespace NetCoreHexagonal.Application.Ports.Out.Persistence
{
    public interface IStudentsRepository
    {
        Task<IReadOnlyList<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(Guid id);
        Task<Student?> GetByNameAsync(StudentName name);
        void Register(Student student);
    }
}
