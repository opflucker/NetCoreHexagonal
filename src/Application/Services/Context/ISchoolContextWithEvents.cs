using NetCoreHexagonal.Application.Ports.Out.Persistence;

namespace NetCoreHexagonal.Application.Services.Context
{
    public interface ISchoolContextWithEvents
    {
        ISchoolContext School { get; }

        Task SaveChangesAndDispatchEventsAsync();
    }
}
