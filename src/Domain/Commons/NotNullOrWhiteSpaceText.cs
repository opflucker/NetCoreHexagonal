namespace NetCoreHexagonal.Domain.Commons
{
    public record class NotNullOrWhiteSpaceText
    {
        private readonly string _name;
        public string Name
        {
            get => _name;
            init => _name = ValidatedName(value);
        }

        public NotNullOrWhiteSpaceText(string name)
        {
            _name = ValidatedName(name);
        }

        private static string ValidatedName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace", nameof(name));
            return name;
        }
    }
}
