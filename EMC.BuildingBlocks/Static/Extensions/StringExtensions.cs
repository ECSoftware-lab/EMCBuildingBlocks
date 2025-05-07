namespace EMC.BuildingBlocks.Static.Extensions
{
    public static class StringExtensions
    {
        private static string Trim(string value)
        {
            return value.Trim();
        }
        private static string Truncate(string value, int length)
        {
            return value.Length > length ? value.Substring(0, length) : value;
        }
        public static string CleanText(this string value, int length)
        {
            return !string.IsNullOrEmpty(value) ? Truncate(Trim(value), length) : value;
        }
        public static string ToLowerCustom(this string input)
        {
            return input?.Trim()?.ToLowerInvariant();
        }
        public static string CapitalizeWords(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            return string.Join(" ", input.Split(' ')
                .Where(w => !string.IsNullOrWhiteSpace(w))
                .Select(w => char.ToUpper(w[0]) + w.Substring(1).ToLower()));
        }

    }
}
