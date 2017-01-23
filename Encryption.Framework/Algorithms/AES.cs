using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EasyEncryption
{
    public abstract class AES
    {
        /// <summary>
        /// Encrypt text using AES algorithm.
        /// </summary>
        /// <param name="plainText">The text that you want to encrypt</param>
        /// <param name="key">Symmetric key that is used for encryption and decryption.</param>
        /// <param name="iv">Initialization vector (IV) for the symmetric algorithm.</param>
        /// <returns>Encrypt base64 string</returns>
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
                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }
        /// <summary>
        /// Decrypt text using AES algorithm.
        /// </summary>
        /// <param name="encryptedText">Your encrypted base64 string</param>
        /// <param name="key">Symmetric key that is used for encryption and decryption.</param>
        /// <param name="iv">Initialization vector (IV) for the symmetric algorithm.</param>
        /// <returns>Decrypted string</returns>
        public static string Decrypt(string encryptedText, string key, string iv)
        {
            var encryptedTextByte = Convert.FromBase64String(encryptedText); ; // parse text to bites array
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
                            csDecrypt.Close();
                            srDecrypt.Close();
                            return res;
                        }
                    }
                }
            }
        }
    }
}
