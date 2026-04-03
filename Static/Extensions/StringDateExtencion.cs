using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMC.BuildingBlocks.Static.Extensions
{
    public static class StringDateExtencion
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

    }
}
