using Microsoft.Extensions.DependencyInjection;
using NetCoreHexagonal.Application.Ports.In;
using NetCoreHexagonal.Application.Ports.In.Dtos;
using NetCoreHexagonal.Application.Ports.Out.EventsDispatching;
using NetCoreHexagonal.Application.Ports.Out.Persistence;
using NetCoreHexagonal.Application.Services;
using NetCoreHexagonal.Domain.Core.Students;
using NetCoreHexagonal.UnitTests.Fakes.EventsDispatching;
using NetCoreHexagonal.UnitTests.Fakes.Persistence;
using System.Threading.Tasks;
using Xunit;

namespace NetCoreHexagonal.UnitTests
{
    public class SchoolServiceTests
    {
        readonly ISchoolContext schoolContext;
        readonly ISchoolService schoolService;

        public SchoolServiceTests()
        {
            var sp = new ServiceCollection()
                .ConfigureApplicationServices()
                .AddScoped<ISchoolContext, SchoolContextFake>()
                .AddScoped<IEventsDispatcher, EventsDispatcherFake>()
                .BuildServiceProvider();

            schoolContext = sp.GetRequiredService<ISchoolContext>();
            schoolService = sp.GetRequiredService<ISchoolService>();
        }

        [Fact]
        public async Task When_Enroll_Student_Then_Success()
        {
            var registerCourseDto1 = new RegisterCourseDto("Math");
            await schoolService.RegisterCourse(registerCourseDto1);

            var registerCourseDto2 = new RegisterCourseDto("Physics");
            await schoolService.RegisterCourse(registerCourseDto2);

            var registerStudentDto = new RegisterStudentDto("Jose", registerCourseDto1.Name);
            await schoolService.RegisterStudent(registerStudentDto);

            await schoolService.EnrollStudent(new EnrollStudentDto(registerStudentDto.Name, registerCourseDto2.Name));

            var student = await schoolContext.Students.GetByNameAsync(registerStudentDto.Name.ToStudentName());
            Assert.NotNull(student);
            Assert.Equal(1, student!.Enrollments.Count);
        }
    }
}