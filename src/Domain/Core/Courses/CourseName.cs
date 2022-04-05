using NetCoreHexagonal.Domain.Commons;

namespace NetCoreHexagonal.Domain.Core.Courses
{
    public sealed record class CourseName(string Name) : NotNullOrWhiteSpaceText(Name);

    public static partial class StringExtensions
    {
        public static CourseName ToCourseName(this string name) => new(name);
    }
}
