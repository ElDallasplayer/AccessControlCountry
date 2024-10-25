using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PrincipalObjects
{
    public static class Security
    {
        public static byte[] _key;
        public static string secretKeyAsBase64 = "B64F1pB9xBD/r1AuW7FTkyFV/ykIDi+/PuoDj9JylaQ=";

        public static void GenerateKey()
        {
            //ONLY GENERATE ONE TIME, AND THEN, SAVE AS BASE64
            using (Aes aes = Aes.Create())
            {
                aes.GenerateKey();
                _key = aes.Key;
                string data = Convert.ToBase64String(_key); //TAKE THIS, AND PUT INTO secretKeyAsBase64
            }
        }

        public static void SetKey(byte[] key)
        {
            _key = key;
        }

        public static byte[] Encrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(secretKeyAsBase64);
                aes.GenerateIV();

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream())
                {
                    ms.Write(aes.IV, 0, aes.IV.Length);
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                    return ms.ToArray();
                }
            }
        }

        public static string Decrypt(byte[] cipherText)
        {
            if (cipherText == null || cipherText.Length < 16)
            {
                throw new ArgumentException("The ciphertext is too short.");
            }

            using (Aes aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(secretKeyAsBase64);

                byte[] iv = new byte[16];
                Array.Copy(cipherText, 0, iv, 0, iv.Length);
                aes.IV = iv;

                try
                {
                    using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                    using (var ms = new MemoryStream(cipherText, iv.Length, cipherText.Length - iv.Length))
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    using (var sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
                catch (CryptographicException ex)
                {
                    Console.WriteLine($"Error durante la desencriptación: {ex.Message}");
                    throw;
                }
            }
        }

        public static string EncryptToBase64(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(secretKeyAsBase64);
                aes.GenerateIV();

                byte[] iv = aes.IV;

                using (var encryptor = aes.CreateEncryptor(aes.Key, iv))
                using (var ms = new MemoryStream())
                {
                    ms.Write(iv, 0, iv.Length);

                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string DecryptFromBase64(string base64CipherText)
        {
            byte[] cipherText = Convert.FromBase64String(base64CipherText);

            if (cipherText == null || cipherText.Length < 16)
            {
                throw new ArgumentException("El ciphertext es demasiado corto.");
            }

            using (Aes aes = Aes.Create())
            {
                byte[] iv = new byte[16];
                Array.Copy(cipherText, 0, iv, 0, iv.Length);
                aes.IV = iv;

                aes.Key = Convert.FromBase64String(secretKeyAsBase64);

                using (var ms = new MemoryStream(cipherText, iv.Length, cipherText.Length - iv.Length))
                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}