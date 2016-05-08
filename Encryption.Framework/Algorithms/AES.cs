using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EasyEncryption.Framework.Algorithms
{
    public static class Aes
    {
        /// <summary>
        /// Encrypt text using AES algorithm.
        /// </summary>
        /// <param name="plainText">The text that you want to encrypt</param>
        /// <param name="key">Symmetric key that is used for encryption and decryption.</param>
        /// <param name="iv">Initialization vector (IV) for the symmetric algorithm.</param>
        /// <returns>Encrypt text string</returns>
        public static string Encrypt(string plainText, string key, string iv)
        {
            var bytes = Encoding.UTF8.GetBytes(plainText); // parse text to bites array
            using (var desCryptoService = new AesCryptoServiceProvider())
            {
                desCryptoService.Key = Encoding.UTF8.GetBytes(key);
                desCryptoService.IV = Encoding.UTF8.GetBytes(iv);

                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, desCryptoService.CreateEncryptor(), CryptoStreamMode.Write); // open cryptoStream
                    cryptoStream.Write(bytes, 0, bytes.Length);
                    cryptoStream.Close();
                    memoryStream.Close();
                    var result = Encoding.Default.GetString(memoryStream.ToArray());
                    return result;
                }
            }
        }
        /// <summary>
        /// Decrypt text using AES algorithm.
        /// </summary>
        /// <param name="encryptedText">Your encrypted string</param>
        /// <param name="key">Symmetric key that is used for encryption and decryption.</param>
        /// <param name="iv">Initialization vector (IV) for the symmetric algorithm.</param>
        /// <returns>Decrypted string</returns>
        public static string Decrypt(string encryptedText, string key, string iv)
        {
            var encryptedTextByte = Encoding.Default.GetBytes(encryptedText); // parse text to bites array
            using (var aesCryptoServiceProvider = new AesCryptoServiceProvider())
            {
                aesCryptoServiceProvider.Key = Encoding.UTF8.GetBytes(key);
                aesCryptoServiceProvider.IV = Encoding.UTF8.GetBytes(iv);
                var decryptor = aesCryptoServiceProvider.CreateDecryptor(aesCryptoServiceProvider.Key, aesCryptoServiceProvider.IV);
                // Create the streams used for decryption.
                using (var msDecrypt = new MemoryStream(encryptedTextByte))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            var res = srDecrypt.ReadToEnd();
                            return res;
                        }
                    }
                }

            }
        }
    }
}
