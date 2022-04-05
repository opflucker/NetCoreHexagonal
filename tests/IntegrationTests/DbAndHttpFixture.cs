using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NetCoreHexagonal.Domain.Core.Courses;
using NetCoreHexagonal.Domain.Core.Students;
using NetCoreHexagonal.Persistence.Design;
using Polly;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetCoreHexagonal.IntegrationTests
{
    public class DbAndHttpFixture : IDisposable
    {
        public HttpClient HttpClient { get; }
        public SchoolDbContext DbContext { get; }

        public DbAndHttpFixture()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            HttpClient = new HttpClient() { BaseAddress = new Uri(config["ApiBaseUrl"]) };

            var connectionString = config.GetConnectionString("DefaultConnection");
            DbContext = BuildDbContext(connectionString).GetAwaiter().GetResult();
        }

        private static async Task<SchoolDbContext> BuildDbContext(string connectionString)
        {
            await EnsureCanConnectToDB(connectionString);

            var dbContext = new SchoolDbContext(connectionString, false);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            await Populate(dbContext);

            return dbContext;
        }

        private static async Task EnsureCanConnectToDB(string connectionString)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            connectionStringBuilder.InitialCatalog = "master";
            var masterConnectionString = connectionStringBuilder.ToString();

            await Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(10, retryCount => TimeSpan.FromSeconds(2))
                .ExecuteAsync(async () =>
                {
                    using var sqlConnection = new SqlConnection(masterConnectionString);
                    await sqlConnection.OpenAsync();
                });
        }

        private static async Task Populate(SchoolDbContext dbContext)
        {
            var courseNames = new[] { "Math", "Physics", "History" };
            var courses = courseNames.Select(name => new Course(name.ToCourseName())).ToArray();
            foreach (var course in courses)
                dbContext.Courses.Add(course);

            var student = new Student("Otto".ToStudentName(), courses.First());
            dbContext.Students.Add(student);

            await dbContext.SaveChangesAsync();
        }

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    HttpClient?.Dispose();
                    DbContext?.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
