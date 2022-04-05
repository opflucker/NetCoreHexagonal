using NetCoreHexagonal.Domain.Commons;

namespace NetCoreHexagonal.Domain.Core.Students
{
    public sealed record StudentEnrolledEvent(StudentName Name) : IEvent;
}
