using System.Security.Cryptography;
using System.Text;

namespace EMC.BuildingBlocks.Static
{
    public static class CryptoHelper
    {

        private static readonly string Key = Environment.GetEnvironmentVariable("MY_AES_KEY") ??
            throw new InvalidOperationException("La clave de encriptación no está configurada.");

        private static readonly byte[] KeyBytes = Encoding.UTF8.GetBytes(Key.Length >= 32
            ? Key.Substring(0, 32)
            : throw new InvalidOperationException("La clave debe tener al menos 32 caracteres para AES-256."));

        public static string Encrypt(string input)
        {
            if (string.IsNullOrEmpty(input)) throw new ArgumentNullException(nameof(input));

            using var aes = Aes.Create();
            aes.Key = KeyBytes;
            aes.GenerateIV(); // IV aleatorio

            using var encryptor = aes.CreateEncryptor();
            using var msEncrypt = new MemoryStream();

            // Primero guardamos el IV
            msEncrypt.Write(aes.IV, 0, aes.IV.Length);

            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(input);
            }

            var encryptedBytes = msEncrypt.ToArray();
            return Convert.ToBase64String(encryptedBytes)
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");
        }

        public static string Decrypt(string encryptedInput)
        {
            if (string.IsNullOrEmpty(encryptedInput)) throw new ArgumentNullException(nameof(encryptedInput));

            string base64Input = encryptedInput.Replace('-', '+').Replace('_', '/');
            switch (base64Input.Length % 4)
            {
                case 2: base64Input += "=="; break;
                case 3: base64Input += "="; break;
            }

            var fullCipher = Convert.FromBase64String(base64Input);

            using var aes = Aes.Create();
            aes.Key = KeyBytes;

            // Extraer IV (los primeros 16 bytes)
            byte[] iv = fullCipher.Take(16).ToArray();
            aes.IV = iv;

            // Extraer el resto (datos cifrados)
            byte[] cipherText = fullCipher.Skip(16).ToArray();

            using var decryptor = aes.CreateDecryptor();
            using var msDecrypt = new MemoryStream(cipherText);
            using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);

            return srDecrypt.ReadToEnd();
        }
    }

}
/*
         powerShel setx MY_AES_KEY "mi_clave_ultra_segura_de_32_chars" 
        export MY_AES_KEY="mi_clave_ultra_segura_de_32_chars" source ~/.bashrc  
         */