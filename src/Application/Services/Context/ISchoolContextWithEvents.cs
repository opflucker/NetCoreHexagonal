using NetCoreHexagonal.Application.Ports.Out.Persistence;

namespace NetCoreHexagonal.Application.Services.Context
{
    internal interface ISchoolContextWithEvents
    {
        ISchoolContext School { get; }

        Task SaveChangesAndDispatchEventsAsync();
    }
}
