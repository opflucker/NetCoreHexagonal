using NetCoreHexagonal.Application.Ports.Out;
using NetCoreHexagonal.Domain.Commons;
using System.Threading.Tasks;

namespace NetCoreHexagonal.UnitTests.Fakes
{
    internal class EventsDispatcherFake : IEventsDispatcher
    {
        public Task DispatchAsync(IEvent @event)
        {
            return Task.CompletedTask;
        }
    }
}
