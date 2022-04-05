using NetCoreHexagonal.Domain.Commons;

namespace NetCoreHexagonal.Domain.Core.Students
{
    public sealed record class StudentName(string Name) : NotNullOrWhiteSpaceText(Name);

    public static partial class StringExtensions
    {
        public static StudentName ToStudentName(this string name) => new(name);
    }
}
