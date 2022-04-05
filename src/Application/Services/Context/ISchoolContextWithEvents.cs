using NetCoreHexagonal.Application.Ports.Out;

namespace NetCoreHexagonal.Application.Services.Context
{
    public interface ISchoolContextWithEvents
    {
        ISchoolContext School { get; }

        Task SaveChangesAndDispatchEventsAsync();
    }
}
