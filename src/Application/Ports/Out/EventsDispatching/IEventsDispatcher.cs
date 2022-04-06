using NetCoreHexagonal.Domain.Commons;

namespace NetCoreHexagonal.Application.Ports.Out.EventsDispatching
{
    public interface IEventsDispatcher
    {
        Task DispatchAsync(IEvent @event);
    }
}
