using Microsoft.Extensions.DependencyInjection;
using NetCoreHexagonal.Application.Ports.In;
using NetCoreHexagonal.Application.Services.Context;

namespace NetCoreHexagonal.Application.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
            => services
                .AddScoped<ISchoolContextWithEvents, SchoolContextWithEvents>()
                .AddScoped<ISchoolService, SchoolService>();
    }
}
