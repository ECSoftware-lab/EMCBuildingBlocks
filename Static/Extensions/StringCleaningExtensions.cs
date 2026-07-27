using Tucu.Common.StringsUtilities;

namespace EMC.BuildingBlocks.Static.Extensions
{
    public static class StringCleaningExtensions
    {
        public static string limpiar(string value, int lengh = 50)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            var newValue = value.CleanAndTruncate(lengh);
            newValue = newValue.CapitalizeWords();
            newValue = newValue.ToAllowedCharactersWithSpaces();
            return newValue;
        }

    }
}
