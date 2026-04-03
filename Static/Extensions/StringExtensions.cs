using System.Globalization;

namespace EMC.BuildingBlocks.Static.Extensions
{
    public static class StringExtensions
    {
        public static DateTime? ParseDateTime(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;

            string[] formatos = {
        "dd/MM/yyyy",
        "dd-MM-yyyy",
        "yyyy/MM/dd",
        "yyyy-MM-dd",
        "dd/MM/yyyy HH:mm:ss",
        "dd-MM-yyyy HH:mm:ss",
        "yyyy/MM/dd HH:mm:ss",
        "yyyy-MM-dd HH:mm:ss",
        "yyyy-MM-ddTHH:mm:ss" // ISO 8601
    };

            if (DateTime.TryParseExact(
                input,
                formatos,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime result))
            {
                return result;
            }

            return null;
        }

        public static string RoleNormalizer(string role)
        {
            return role.Trim().ToUpperInvariant();
        }

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
