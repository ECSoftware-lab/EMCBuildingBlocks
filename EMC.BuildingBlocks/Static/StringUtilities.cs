using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace EMC.BuildingBlocks.Static
{
    public class StringUtilities
    {
        public static string CleanText(string value, int length)
        {
            return !string.IsNullOrEmpty(value) ? Truncate(Trim(value), length) : value;
        }
        public static string CleanText(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            return new string(value.Where(c => !char.IsWhiteSpace(c)).ToArray());
        }
        private static string Truncate(string value, int length)
        {
            return value.Length > length ? Trim(value.Substring(0, length)) : value;
        }
        public static string LimpiarEspacios(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            // Quita espacios al principio y al final, y reduce múltiples espacios internos
            return string.Join(" ", input.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
        }
        private static string Trim(string value)
        {
            return value.Trim();
        }

        public static string ToCuitFormat(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            return input.Length < 11 ? input : input.Insert(2, "-").Insert(input.Length, "-");
        }

        public static string ToAlphanumericWithDots(string text, int longText, string replaceFor = ".")
        {
            /*Esta función es útil para normalizar textos al eliminar caracteres especiales y limitar su longitud.
            El comportamiento predeterminado de reemplazar 
            con puntos puede ser modificado al pasar un argumento diferente para replaceFor.*/
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            var cad = string.IsNullOrEmpty(text) ? "" : Regex.Replace(text, "[^a-zA-Z,^0-9]", replaceFor);
            string firstCharacters =
            !string.IsNullOrEmpty(cad) && cad.Length >= longText
            ? cad.Substring(0, longText)
            : cad;
            firstCharacters = firstCharacters.Last() == '.' ? firstCharacters.Remove(firstCharacters.Length - 1) : firstCharacters;
            return firstCharacters;
        }
        public static string ToAllowedCharactersWithSpaces(string toModify)
        {
            var toReturn = string.Empty;

            if (string.IsNullOrWhiteSpace(toModify))
                return toReturn;

            // Mantener letras, dígitos, guiones bajos, guiones, espacios en blanco y las letras ñ/Ñ
            toModify = new string(toModify
                .Where(c => char.IsLetterOrDigit(c) || c == '_' || c == '-' || char.IsWhiteSpace(c) || c == 'ñ' || c == 'Ñ')
                .ToArray());

            // No aplicar normalización aquí para evitar eliminar las letras ñ/Ñ
            toReturn = new string(toModify).Trim();

            return toReturn;
        }

        public static string ToAllowedCharacters(string toModify)
        {
            //igual a ToNonSpecialCharacters pero acepta digitos guiones 
            var toReturn = string.Empty;

            if (string.IsNullOrWhiteSpace(toModify))
                return toReturn;

            toModify = CleanText(toModify);
            if (string.IsNullOrWhiteSpace(toModify))
                return toReturn;
            // Remove any character that is not a letter, digit, whitespace, underscore, or hyphen
            toModify = new string(toModify.Where(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || c == '_' || c == '-').ToArray());

            toReturn = new string
            (
                toModify.Normalize(NormalizationForm.FormD)
                    .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    .ToArray()
            ).Normalize(NormalizationForm.FormC).Trim();

            return toReturn;
        }

        public static string ToNonSpecialCharacters(string toModify)
        {//elimina todos los caracteres que no son letras o espacios en blanco, incluyendo los dígitos.
            var toReturn = string.Empty;

            if (string.IsNullOrWhiteSpace(toModify))
                return toReturn;

            toModify = new string(toModify.Where(c => char.IsLetter(c) || char.IsWhiteSpace(c)).ToArray());

            toReturn = new string
            (
                toModify.Normalize(NormalizationForm.FormD)
                    .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    .ToArray()
            ).Normalize(NormalizationForm.FormC).Trim();

            return toReturn;
        }
        public static string RemoveNonDigits(string input)
        {
            // Remove spaces, dots, commas, semicolons, and any non-digit characters
            string result = Regex.Replace(input, @"[^\d]", "");

            return result;
        }
        public static string RemovePrefixBase64(string text)
        {
            int commaIndex = text.IndexOf(',');
            if (commaIndex >= 0)
            {
                return text.Substring(commaIndex + 1);
            }
            else
            {
                return text;
            }
        }
        public static bool ValidateBase64(string? cadena)
        {
            if (string.IsNullOrWhiteSpace(cadena))
            {
                return false;
            }

            try
            {
                byte[] data = Convert.FromBase64String(cadena);
                string base64String = Convert.ToBase64String(data);
                return cadena.Equals(base64String);
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public static string ExtracTypeBase64(string dataUrl)
        {
            string imageType = string.Empty;
            string[] parts = dataUrl.Split(',');
            if (parts.Length == 2 && parts[0].StartsWith("data:image/"))
            {
                string[] typeParts = parts[0].Split('/');
                if (typeParts.Length == 2)
                {
                    imageType = typeParts[1].Split(';')[0]; // Aquí obtienes el tipo de imagen (jpeg, png, gif, etc.)

                }
            }
            return imageType;
        }
        public static string ValidateAndCleanExtension(string extension, List<string> validExtensions)
        {
            // Eliminar el punto "." del inicio, si existe
            extension = extension.TrimStart('.');

            if (validExtensions.Contains(extension.ToLower()))
            {
                return extension.ToLower(); // Devuelve la extensión sin el "."
            }
            //else
            //{
            //    throw new NotValidExtensionException("La extensión del archivo no es válida.");
            //}
            return string.Empty;
        }

        public static Guid ToGuidOrDefault(string input)
        {
            if (Guid.TryParse(input, out Guid result))
            {
                return result;
            }
            else
            {
                return Guid.Empty;
            }
        }
        public static bool IsGuid(string input)
        {
            var resp = ToGuidOrDefault(input);
            return resp != Guid.Empty;

        }
        public static decimal ConvertToDecimal(string input)
        {
            decimal number;
            // Reemplaza la coma por un punto
            string normalizedInput = input.Replace(',', '.');

            // Convierte a decimal usando CultureInfo.InvariantCulture
            //return decimal.Parse(normalizedInput, CultureInfo.InvariantCulture);


            if (decimal.TryParse(normalizedInput, NumberStyles.Any, CultureInfo.InvariantCulture, out number))
            {
                return number;
            }
            throw new FormatException("El formato del número no es válido.");

            //if (decimal.TryParse(input, NumberStyles.Any, new CultureInfo("es-ES"), out number))
            //{
            //    return number;
            //}

            //throw new FormatException("El formato del número no es válido.");
        }

        public static DateTime ConvertToDateTimeOrDefault(string input)
        {
            // Intentar convertir el string a DateTime
            if (DateTime.TryParse(input, out DateTime result))
            {
                return result; // Si es válido, retorna la fecha convertida
            }

            // Si es nulo o no es una fecha válida, retorna la fecha de hoy
            return DateTime.Now;
        }

        public static string CapitalizeWords(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            return string.Join(" ", input.Split(' ')
                .Where(w => !string.IsNullOrWhiteSpace(w))
                .Select(w => char.ToUpper(w[0]) + w.Substring(1).ToLower()));
        }

        public static (string nombreArchivo, string token) ExtraerNombreYToken(string url)
        {

            if (string.IsNullOrEmpty(url))
                throw new ArgumentException("La URL proporcionada es inválida.");

            Uri uri = new Uri(url);
            string path = uri.AbsolutePath;
            string nombreArchivo = path.Substring(path.LastIndexOf('/') + 1);
            string query = uri.Query;
            var queryParameters = HttpUtility.ParseQueryString(query);
            string token = queryParameters["token"];
            if (string.IsNullOrEmpty(token))
                throw new ArgumentException("El token no se encontró en la URL proporcionada.");
            return (nombreArchivo, token);
        }
        public static (string Directorio, string Nombre) ExtraerDirectorioYNombre(string ruta)
        {
            if (string.IsNullOrEmpty(ruta))
                throw new ArgumentException("La ruta proporcionada es inválida.");

            // Dividir la ruta por '/'
            string[] partes = ruta.Split('/');

            // Verificar que la ruta tenga al menos el formato esperado
            if (partes.Length < 2)
                throw new ArgumentException("La ruta proporcionada no tiene el formato esperado.");

            // Obtener el directorio y el nombre del archivo
            string directorio = partes[partes.Length - 2]; // penúltimo segmento (directorio)
            string nombre = partes[partes.Length - 1]; // último segmento (nombre del archivo)

            return (directorio, nombre);
        }
        public static string GetImageUrl(string imagenName, string imagenToken, string directorio)
        {
            if (string.IsNullOrEmpty(imagenName) || string.IsNullOrEmpty(imagenToken) || string.IsNullOrEmpty(directorio))
            {
                throw new ArgumentException("Todos los parámetros deben ser válidos.");
            }

            // Obtener solo la parte del archivo desde imagenName
            string fileName = imagenName.Substring(imagenName.IndexOf(directorio, StringComparison.Ordinal) + directorio.Length + 1);

            // Codificar el nombre del archivo (reemplazar / por %2F para la URL)
            string encodedFileName = HttpUtility.UrlEncode(fileName)?.Replace("+", "%20");

            // Construir la URL
            return $"https://firebasestorage.googleapis.com/v0/b/{imagenName.Substring(0, imagenName.IndexOf('/'))}/o/{directorio}%2F{encodedFileName}?alt=media&token={imagenToken}";
        }

        public static bool ValidarCuitCuil(string numero)
        {
            // Eliminar guiones o espacios
            numero = numero.Replace("-", "").Trim();

            // Validar formato (debe tener 11 dígitos)
            if (numero.Length != 11 || !long.TryParse(numero, out _))
                return false;

            // Pesos según la posición
            int[] pesos = { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };

            // Calcular suma de los productos
            int suma = 0;
            for (int i = 0; i < 10; i++)
            {
                suma += int.Parse(numero[i].ToString()) * pesos[i];
            }

            // Calcular módulo 11
            int resto = suma % 11;
            int digitoVerificador = 11 - resto;

            // Ajustar si es 10 o 11
            if (digitoVerificador == 10) return false; // Número inválido
            if (digitoVerificador == 11) digitoVerificador = 0;

            // Comparar el dígito verificador calculado con el proporcionado
            return digitoVerificador == int.Parse(numero[10].ToString());
        }

        public static bool ValidarDNI(string dni)
        {
            // Eliminar espacios en blanco
            dni = dni.Trim();

            // Verificar que solo contenga números y tenga entre 7 y 8 dígitos
            if (!long.TryParse(dni, out _) || dni.Length < 7 || dni.Length > 8)
            {
                return false;
            }

            // Convertir a número y verificar rango
            long numeroDni = long.Parse(dni);
            if (numeroDni < 1000000 || numeroDni > 99999999)
            {
                return false;
            }

            return true;
        }


    }
}
