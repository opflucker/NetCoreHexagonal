using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace NetCoreHexagonal.Persistence.Design
{
    public sealed class SchoolDbContextFactory : IDesignTimeDbContextFactory<SchoolDbContext>
    {
        public SchoolDbContext CreateDbContext(string[] args)
        {
            string basePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName, "Presentation.Console");

            var connectionString = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build()
                .GetConnectionString("DefaultConnection");

            return new SchoolDbContext(connectionString, true);
        }
    }
}
