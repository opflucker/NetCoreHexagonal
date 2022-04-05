using Microsoft.Extensions.DependencyInjection;
using NetCoreHexagonal.Application.Ports.Out;

namespace NetCoreHexagonal.EventsDispatching
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureEventsDispatchingServices(this IServiceCollection services) 
            => services.AddScoped<IEventsDispatcher, ConsoleEventsDispatcher>();
    }
}
