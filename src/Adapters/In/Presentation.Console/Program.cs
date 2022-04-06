using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreHexagonal.Application.Ports.In;
using NetCoreHexagonal.Application.Ports.In.Dtos;
using NetCoreHexagonal.Application.Ports.Out.Persistence;
using NetCoreHexagonal.Application.Services;
using NetCoreHexagonal.Domain.Core.Students;
using NetCoreHexagonal.EventsDispatching;
using NetCoreHexagonal.Persistence;
using NetCoreHexagonal.Persistence.Design;

var connectionString = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build()
    .GetConnectionString("DefaultConnection");

var sp = new ServiceCollection()
    .ConfigureApplicationServices()
    .ConfigurePersistenceServices(connectionString, true)
    .ConfigureEventsDispatchingServices()
    .BuildServiceProvider();

using (var schoolDbContext = sp.GetRequiredService<SchoolDbContext>())
{
    await schoolDbContext.Database.EnsureDeletedAsync();
    await schoolDbContext.Database.EnsureCreatedAsync();
}

var schoolService = sp.GetRequiredService<ISchoolService>();

var courseNames = new[] { "Math", "Physics", "History" };
foreach (var name in courseNames)
    await schoolService.RegisterCourse(new RegisterCourseDto(name));

await schoolService.RegisterStudent(new RegisterStudentDto("Otto", courseNames.First()));
await schoolService.EnrollStudent(new EnrollStudentDto("Otto", "Physics"));

var schoolContext = sp.GetRequiredService<ISchoolContext>();
var student = await schoolContext.Students.GetByNameAsync("Otto".ToStudentName());
if (student != null)
{
    Console.WriteLine($"{student.Name}, with enrollments:");
    foreach (var e in student.Enrollments)
        Console.WriteLine(e.Course.Name);
}
