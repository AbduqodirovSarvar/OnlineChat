using Microsoft.Extensions.Configuration;
using OnlineChat.Application.Abstractions;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace OnlineChat.Application.Services
{
    public class EncryptionService : IEncryptionService
    {
        private readonly byte[] _key;

        public EncryptionService(IConfiguration configuration)
        {
            var key = configuration["EncryptionKey"];
            if (string.IsNullOrEmpty(key) || key.Length != 32)
                throw new ArgumentException("Key must be 32 characters long.");
            _key = Encoding.UTF8.GetBytes(key);
        }

        public (string encryptedMessage, byte[] iv) Encrypt(string message)
        {
            using var aes = Aes.Create();
            aes.Key = _key;
            aes.GenerateIV();
            var iv = aes.IV;

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(message);
            }

            var encryptedMessage = Convert.ToBase64String(ms.ToArray());
            return (encryptedMessage, iv);
        }

        public string Decrypt(string encryptedMessage, byte[] iv)
        {
            try
            {
                using var aes = Aes.Create();
                aes.Key = _key;
                aes.IV = iv;

                using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using var ms = new MemoryStream(Convert.FromBase64String(encryptedMessage));
                using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                using var sr = new StreamReader(cs);
                var res = sr.ReadToEnd();
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error decrypting message: " + ex.Message);
                return string.Empty;
            }
        }

    }
}
