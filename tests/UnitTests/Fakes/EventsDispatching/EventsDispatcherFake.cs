using NetCoreHexagonal.Application.Ports.Out.EventsDispatching;
using NetCoreHexagonal.Domain.Commons;
using System.Threading.Tasks;

namespace NetCoreHexagonal.UnitTests.Fakes.EventsDispatching
{
    internal class EventsDispatcherFake : IEventsDispatcher
    {
        public Task DispatchAsync(IEvent @event)
        {
            return Task.CompletedTask;
        }
    }
}
