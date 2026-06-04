namespace EMC.BuildingBlocks.Static.Extensions
{
    // Application/Helpers/PhoneNormalizer.cs
    public static class PhoneNormalizer
    {
        // Países soportados: código → reglas específicas
        // Expandir según los países que maneje tu sistema
        private static readonly Dictionary<string, CountryPhoneRule> Rules = new()
        {
            ["AR"] = new CountryPhoneRule
            {
                CountryCode = "54",
                AreaMinLength = 2,
                AreaMaxLength = 4,
                NumberLength = 8,
                // Argentina móvil: entre el código de país y el área va el 9
                MobilePrefix = "9",
                StripLeading = ["0", "15"] // sacar 0 del área y 15 del número
            },
            ["BR"] = new CountryPhoneRule
            {
                CountryCode = "55",
                AreaMinLength = 2,
                AreaMaxLength = 2,
                NumberLength = 9,
                MobilePrefix = null,
                StripLeading = ["0"]
            },
            ["UY"] = new CountryPhoneRule
            {
                CountryCode = "598",
                AreaMinLength = 2,
                AreaMaxLength = 2,
                NumberLength = 7,
                MobilePrefix = null,
                StripLeading = ["0"]
            }
        };

        /// <summary>
        /// Normaliza y valida un teléfono a formato E.164 sin +
        /// Lanza PhoneValidationException si no se puede normalizar.
        /// </summary>
        public static string NormalizeAndValidate(string rawPhone, string countryCode = "AR")
        {
            if (string.IsNullOrWhiteSpace(rawPhone))
                throw new PhoneValidationException("El teléfono no puede estar vacío.");

            // 1. sacar todo lo que no sea dígito (espacios, guiones, paréntesis, +)
            var digits = new string(rawPhone.Where(char.IsDigit).ToArray());

            if (digits.Length < 6)
                throw new PhoneValidationException($"Número demasiado corto: '{rawPhone}'");

            // 2. si ya viene con código de país completo y correcto, solo validar longitud
            var rule = GetRule(countryCode);
            if (digits.StartsWith(rule.CountryCode))
            {
                // ya tiene el código de país — validar largo total
                ValidateLength(digits, rule);
                return digits;
            }

            // 3. aplicar reglas del país para normalizar
            digits = ApplyCountryRules(digits, rule);

            // 4. agregar código de país
            digits = rule.CountryCode + digits;

            // 5. validar largo final
            ValidateLength(digits, rule);

            return digits;
        }

        /// <summary>
        /// Solo verifica si el formato ya normalizado es válido.
        /// No lanza — devuelve bool. Útil para validación de DTO.
        /// </summary>
        public static bool IsValid(string rawPhone, string countryCode = "AR")
        {
            try
            {
                NormalizeAndValidate(rawPhone, countryCode);
                return true;
            }
            catch (PhoneValidationException)
            {
                return false;
            }
        }

        // ── privados ───────────────────────────────────────────

        private static string ApplyCountryRules(string digits, CountryPhoneRule rule)
        {
            // sacar prefijos locales configurados (ej: "0", "15")
            foreach (var strip in rule.StripLeading)
            {
                if (digits.StartsWith(strip))
                {
                    digits = digits[strip.Length..];
                    break; // solo sacar el primero que matchee
                }
            }

            // Argentina: móviles necesitan el 9 entre código país y área
            // ej: 3817545008063 → 9 + 3817545008063
            if (rule.MobilePrefix is not null && !digits.StartsWith(rule.MobilePrefix))
            {
                // heurística: si la longitud sin el 9 cuadra → agregar el 9
                var withPrefix = rule.MobilePrefix + digits;
                var expectedLen = rule.CountryCode.Length
                                + rule.MobilePrefix.Length
                                + rule.AreaMaxLength
                                + rule.NumberLength;

                if ((rule.CountryCode + withPrefix).Length == expectedLen)
                    digits = withPrefix;
            }

            return digits;
        }

        private static void ValidateLength(string digits, CountryPhoneRule rule)
        {
            // largo mínimo y máximo según país
            int minLen = rule.CountryCode.Length
                       + (rule.MobilePrefix?.Length ?? 0)
                       + rule.AreaMinLength
                       + rule.NumberLength;

            int maxLen = rule.CountryCode.Length
                       + (rule.MobilePrefix?.Length ?? 0)
                       + rule.AreaMaxLength
                       + rule.NumberLength;

            if (digits.Length < minLen || digits.Length > maxLen)
                throw new PhoneValidationException(
                    $"Longitud inválida para el número '{digits}' " +
                    $"(esperado entre {minLen} y {maxLen} dígitos, tiene {digits.Length}).");
        }

        private static CountryPhoneRule GetRule(string countryCode)
        {
            if (!Rules.TryGetValue(countryCode.ToUpperInvariant(), out var rule))
                throw new PhoneValidationException($"País no soportado: '{countryCode}'");
            return rule;
        }

        private class CountryPhoneRule
        {
            public string CountryCode { get; init; } = "";
            public int AreaMinLength { get; init; }
            public int AreaMaxLength { get; init; }
            public int NumberLength { get; init; }
            public string? MobilePrefix { get; init; }
            public string[] StripLeading { get; init; } = [];
        }
    }

    public class PhoneValidationException : Exception
    {
        public PhoneValidationException(string message) : base(message) { }
    }
}
