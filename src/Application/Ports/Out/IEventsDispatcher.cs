using NetCoreHexagonal.Domain.Commons;

namespace NetCoreHexagonal.Application.Ports.Out
{
    public interface IEventsDispatcher
    {
        Task DispatchAsync(IEvent @event);
    }
}
