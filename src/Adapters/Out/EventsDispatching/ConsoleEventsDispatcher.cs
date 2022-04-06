using NetCoreHexagonal.Application.Ports.Out.EventsDispatching;
using NetCoreHexagonal.Domain.Commons;

namespace NetCoreHexagonal.EventsDispatching
{
    internal class ConsoleEventsDispatcher : IEventsDispatcher
    {
        public Task DispatchAsync(IEvent @event)
        {
            Console.WriteLine($"Dispatched event: {@event}");
            return Task.CompletedTask;
        }
    }
}
