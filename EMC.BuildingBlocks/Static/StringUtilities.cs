using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace EMC.BuildingBlocks.Static
{
    
public class StringUtilities
    {
        /// <summary>
        /// Elimina espacios y trunca el texto al largo especificado.
        /// </summary>
        public static string CleanText(string value, int length)
        {
            return !string.IsNullOrEmpty(value) ? Truncate(Trim(value), length) : value;
        }

        /// <summary>
        /// Elimina todos los espacios del texto. Si es nulo o vacío, devuelve cadena vacía.
        /// </summary>
        public static string CleanText(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            return new string(value.Where(c => !char.IsWhiteSpace(c)).ToArray());
        }

        /// <summary>
        /// Trunca el texto al largo indicado y elimina espacios al inicio y final.
        /// </summary>
        private static string Truncate(string value, int length)
        {
            return value.Length > length ? Trim(value.Substring(0, length)) : value;
        }

        /// <summary>
        /// Elimina espacios al inicio y final, y reduce múltiples espacios internos a uno solo.
        /// </summary>
        public static string LimpiarEspacios(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            return string.Join(" ", input.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
        }

        /// <summary>
        /// Elimina espacios al inicio y final.
        /// </summary>
        private static string Trim(string value)
        {
            return value.Trim();
        }

        /// <summary>
        /// Formatea un CUIT agregando guiones en la posición estándar.
        /// </summary>
        public static string ToCuitFormat(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            return input.Length < 11 ? input : input.Insert(2, "-").Insert(input.Length, "-");
        }

        /// <summary>
        /// Reemplaza caracteres especiales por puntos (u otro carácter) y limita la longitud del texto.
        /// </summary>
        public static string ToAlphanumericWithDots(string text, int longText, string replaceFor = ".")
        {
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

        /// <summary>
        /// Conserva letras, dígitos, guiones, guiones bajos, espacios y letras ñ/Ñ.
        /// </summary>
        public static string ToAllowedCharactersWithSpaces(string toModify)
        {
            var toReturn = string.Empty;

            if (string.IsNullOrWhiteSpace(toModify))
                return toReturn;

            toModify = new string(toModify
                .Where(c => char.IsLetterOrDigit(c) || c == '_' || c == '-' || char.IsWhiteSpace(c) || c == 'ñ' || c == 'Ñ')
                .ToArray());

            toReturn = new string(toModify).Trim();

            return toReturn;
        }

        /// <summary>
        /// Elimina caracteres especiales, conserva letras, dígitos, guiones y guiones bajos.
        /// </summary>
        public static string ToAllowedCharacters(string toModify)
        {
            var toReturn = string.Empty;

            if (string.IsNullOrWhiteSpace(toModify))
                return toReturn;

            toModify = CleanText(toModify);
            if (string.IsNullOrWhiteSpace(toModify))
                return toReturn;

            toModify = new string(toModify.Where(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || c == '_' || c == '-').ToArray());

            toReturn = new string
            (
                toModify.Normalize(NormalizationForm.FormD)
                    .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    .ToArray()
            ).Normalize(NormalizationForm.FormC).Trim();

            return toReturn;
        }

        /// <summary>
        /// Elimina todos los caracteres que no son letras o espacios. Excluye dígitos y símbolos.
        /// </summary>
        public static string ToNonSpecialCharacters(string toModify)
        {
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

        /// <summary>
        /// Elimina todos los caracteres que no sean dígitos.
        /// </summary>
        public static string RemoveNonDigits(string input)
        {
            return Regex.Replace(input, @"[^\d]", "");
        }

        /// <summary>
        /// Elimina el prefijo de una cadena Base64 (por ejemplo, 'data:image/png;base64,').
        /// </summary>
        public static string RemovePrefixBase64(string text)
        {
            int commaIndex = text.IndexOf(',');
            return commaIndex >= 0 ? text.Substring(commaIndex + 1) : text;
        }

        /// <summary>
        /// Valida si una cadena es una representación válida de Base64.
        /// </summary>
        public static bool ValidateBase64(string? cadena)
        {
            if (string.IsNullOrWhiteSpace(cadena))
                return false;

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

        /// <summary>
        /// Extrae el tipo MIME de una cadena Base64 (por ejemplo, 'jpeg', 'png').
        /// </summary>
        public static string ExtracTypeBase64(string dataUrl)
        {
            string imageType = string.Empty;
            string[] parts = dataUrl.Split(',');
            if (parts.Length == 2 && parts[0].StartsWith("data:image/"))
            {
                string[] typeParts = parts[0].Split('/');
                if (typeParts.Length == 2)
                {
                    imageType = typeParts[1].Split(';')[0];
                }
            }
            return imageType;
        }

        /// <summary>
        /// Valida y limpia una extensión de archivo. Devuelve la extensión en minúsculas si es válida.
        /// </summary>
        public static string ValidateAndCleanExtension(string extension, List<string> validExtensions)
        {
            extension = extension.TrimStart('.');

            if (validExtensions.Contains(extension.ToLower()))
            {
                return extension.ToLower();
            }

            return string.Empty;
        }

        /// <summary>
        /// Convierte una cadena en Guid. Si no es válida, devuelve Guid.Empty.
        /// </summary>
        public static Guid ToGuidOrDefault(string input)
        {
            return Guid.TryParse(input, out Guid result) ? result : Guid.Empty;
        }

        /// <summary>
        /// Verifica si una cadena representa un Guid válido.
        /// </summary>
        public static bool IsGuid(string input)
        {
            var resp = ToGuidOrDefault(input);
            return resp != Guid.Empty;
        }



        /// <summary>
        /// Convierte una cadena a decimal, reemplazando comas por puntos y usando cultura invariante.
        /// Lanza excepción si el formato no es válido.
        /// </summary>
        public static decimal ConvertToDecimal(string input)
        {
            decimal number;
            string normalizedInput = input.Replace(',', '.');

            if (decimal.TryParse(normalizedInput, NumberStyles.Any, CultureInfo.InvariantCulture, out number))
            {
                return number;
            }

            throw new FormatException("El formato del número no es válido.");
        }

        /// <summary>
        /// Convierte una cadena a DateTime. Si no es válida, retorna la fecha actual.
        /// </summary>
        public static DateTime ConvertToDateTimeOrDefault(string input)
        {
            if (DateTime.TryParse(input, out DateTime result))
            {
                return result;
            }

            return DateTime.Now;
        }

        /// <summary>
        /// Capitaliza cada palabra de la cadena: primera letra en mayúscula, resto en minúscula.
        /// </summary>
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

        /// <summary>
        /// Extrae el nombre del archivo y el token de una URL con parámetros.
        /// </summary>
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

        /// <summary>
        /// Extrae el nombre del archivo y su directorio desde una ruta tipo URL.
        /// </summary>
        public static (string Directorio, string Nombre) ExtraerDirectorioYNombre(string ruta)
        {
            if (string.IsNullOrEmpty(ruta))
                throw new ArgumentException("La ruta proporcionada es inválida.");

            string[] partes = ruta.Split('/');

            if (partes.Length < 2)
                throw new ArgumentException("La ruta proporcionada no tiene el formato esperado.");

            string directorio = partes[partes.Length - 2];
            string nombre = partes[partes.Length - 1];

            return (directorio, nombre);
        }

        /// <summary>
        /// Construye una URL pública de Firebase Storage a partir del nombre de imagen, token y directorio.
        /// </summary>
        public static string GetImageUrl(string imagenName, string imagenToken, string directorio)
        {
            if (string.IsNullOrEmpty(imagenName) || string.IsNullOrEmpty(imagenToken) || string.IsNullOrEmpty(directorio))
            {
                throw new ArgumentException("Todos los parámetros deben ser válidos.");
            }

            string fileName = imagenName.Substring(imagenName.IndexOf(directorio, StringComparison.Ordinal) + directorio.Length + 1);
            string encodedFileName = HttpUtility.UrlEncode(fileName)?.Replace("+", "%20");

            return $"https://firebasestorage.googleapis.com/v0/b/{imagenName.Substring(0, imagenName.IndexOf('/'))}/o/{directorio}%2F{encodedFileName}?alt=media&token={imagenToken}";
        }

        /// <summary>
        /// Valida un número de CUIT/CUIL verificando longitud, formato y dígito verificador.
        /// </summary>
        public static bool ValidarCuitCuil(string numero)
        {
            numero = numero.Replace("-", "").Trim();

            if (numero.Length != 11 || !long.TryParse(numero, out _))
                return false;

            int[] pesos = { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
            int suma = 0;

            for (int i = 0; i < 10; i++)
            {
                suma += int.Parse(numero[i].ToString()) * pesos[i];
            }

            int resto = suma % 11;
            int digitoVerificador = 11 - resto;

            if (digitoVerificador == 10) return false;
            if (digitoVerificador == 11) digitoVerificador = 0;

            return digitoVerificador == int.Parse(numero[10].ToString());
        }

        /// <summary>
        /// Valida un número de DNI asegurando que sea numérico y esté dentro del rango permitido.
        /// </summary>
        public static bool ValidarDNI(string dni)
        {
            dni = dni.Trim();

            if (!long.TryParse(dni, out _) || dni.Length < 7 || dni.Length > 8)
            {
                return false;
            }

            long numeroDni = long.Parse(dni);
            return numeroDni >= 1000000 && numeroDni <= 99999999;
        }

        /// <summary>
        /// Genera un hash SHA256 en Base64 a partir de un token de texto.
        /// </summary>
        public static string HashToken(string token)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(token);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

    }
}
