using NetCoreHexagonal.Application.Ports.Out.EventsDispatching;
using NetCoreHexagonal.Application.Ports.Out.Persistence;

namespace NetCoreHexagonal.Application.Services.Context
{
    internal sealed class SchoolContextWithEvents : ISchoolContextWithEvents
    {
        private readonly ISchoolContext schoolContext;
        private readonly IEventsDispatcher eventsDispatcher;

        public SchoolContextWithEvents(ISchoolContext schoolContext, IEventsDispatcher eventsDispatcher)
        {
            this.schoolContext = schoolContext;
            this.eventsDispatcher = eventsDispatcher;
        }

        public ISchoolContext School => schoolContext;

        public async Task SaveChangesAndDispatchEventsAsync()
        {
            var savedAggregates = await schoolContext.SaveChangesAsync();

            foreach (var aggregate in savedAggregates.Where(a => a.Events.Any()))
            {
                foreach (var @event in aggregate.Events)
                {
                    await eventsDispatcher.DispatchAsync(@event);
                }

                aggregate.ClearEvents();
            }
        }
    }
}
