using System.Security.Cryptography;
using System.Text;

namespace LibraryBookingSystem.Services
{
    public static class TextDecryptor
    {
        private static readonly string Key = "X9@cL#2rFbTz6GvAq7PzY!8eKdW1LmC3";
        private static readonly string IV = "Np6#Tg1$Xr8@Vz3L";

        public static string EncryptText(string text)
        {
            using var encryptionStandard = Aes.Create();
            encryptionStandard.Key = Encoding.UTF8.GetBytes(Key);
            encryptionStandard.IV = Encoding.UTF8.GetBytes(IV);

            var encryptor = encryptionStandard.CreateEncryptor();
            var textBytes = Encoding.UTF8.GetBytes(text);
            var encryptedBytes = encryptor.TransformFinalBlock(textBytes, 0, textBytes.Length);

            return Convert.ToBase64String(encryptedBytes);
        }

        public static string DecryptText(string text)
        {
            using var encryptionStandard = Aes.Create();
            encryptionStandard.Key = Encoding.UTF8.GetBytes(Key);
            encryptionStandard.IV = Encoding.UTF8.GetBytes(IV);

            var decryptor = encryptionStandard.CreateDecryptor();
            var encryptedBytes = Convert.FromBase64String(text);
            var textBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

            return Encoding.UTF8.GetString(textBytes);
        }
    }
}
