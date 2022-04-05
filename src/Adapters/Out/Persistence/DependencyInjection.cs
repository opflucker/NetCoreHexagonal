using Microsoft.Extensions.DependencyInjection;
using NetCoreHexagonal.Application.Ports.Out;
using NetCoreHexagonal.Persistence.Design;

namespace NetCoreHexagonal.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, string connectionString, bool useConsoleLogger) 
            => services
                .AddScoped(_ => new SchoolDbContext(connectionString, useConsoleLogger))
                .AddScoped<ISchoolContext, SchoolContext>();
    }
}
