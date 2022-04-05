using Microsoft.AspNetCore.Mvc;
using NetCoreHexagonal.Application.Ports.In;
using NetCoreHexagonal.Application.Ports.In.Dtos;

namespace NetCoreHexagonal.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class SchoolController : ControllerBase
    {
        private readonly ILogger<SchoolController> logger;
        private readonly ISchoolService schoolService;

        public SchoolController(ILogger<SchoolController> logger, ISchoolService schoolService)
        {
            this.logger = logger;
            this.schoolService = schoolService;
        }

        [HttpGet("courses")]
        public async Task<ActionResult> GetAllCourses()
        {
            var courses = await schoolService.GetAllCourses();

            return Ok(courses);
        }

        [HttpPost("courses")]
        public async Task<ActionResult> RegisterCourse([FromBody] RegisterCourseDto dto)
        {
            logger.LogInformation($"Registering {dto}...");

            var courseDto = await schoolService.RegisterCourse(dto);

            return Ok(courseDto);
        }

        [HttpGet("students")]
        public async Task<ActionResult> GetAllStudents()
        {
            var students = await schoolService.GetAllStudents();

            return Ok(students);
        }

        [HttpPost("students")]
        public async Task<ActionResult> RegisterStudent([FromBody] RegisterStudentDto dto)
        {
            logger.LogInformation($"Registering {dto}...");

            var studentDto = await schoolService.RegisterStudent(dto);

            return Ok(studentDto);
        }

        [HttpPost("enrolls")]
        public async Task<ActionResult> EnrollStudent([FromBody] EnrollStudentDto dto)
        {
            logger.LogInformation($"Enrolling {dto}...");

            await schoolService.EnrollStudent(dto);

            return NoContent();
        }
    }
}