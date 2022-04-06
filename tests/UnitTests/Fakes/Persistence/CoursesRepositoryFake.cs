using NetCoreHexagonal.Application.Ports.Out.Persistence;
using NetCoreHexagonal.Domain.Core.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreHexagonal.UnitTests.Fakes.Persistence
{
    internal class CoursesRepositoryFake : ICoursesRepository
    {
        private readonly Dictionary<string, Course> courses = new();

        public Task<IReadOnlyList<Course>> GetAllAsync()
        {
            return Task.FromResult(courses.Values.ToList() as IReadOnlyList<Course>);
        }

        public ValueTask<Course?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Course?> GetByNameAsync(CourseName name)
        {
            if (courses.TryGetValue(name.Name, out Course? course))
                return Task.FromResult<Course?>(course);
            return Task.FromResult<Course?>(null);
        }

        public void Register(Course course)
        {
            courses.Add(course.Name.Name, course);
        }

        Task<Course?> ICoursesRepository.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
