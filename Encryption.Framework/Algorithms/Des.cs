using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EasyEncryption
{
    public abstract class DES
    {
        /// <summary>
        /// Encrypt text using DES algorithm.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key">Symmetric key that is used for encryption and decryption.</param>
        /// <param name="iv">Initialization vector (IV) for the symmetric algorithm.</param>
        /// <returns></returns>
        public static string Encrypt(string text, string key, string iv)
        {
            var pText = Encoding.UTF8.GetBytes(text);
            using (var desCryptoService = new DESCryptoServiceProvider())
            {
                desCryptoService.Key = Encoding.ASCII.GetBytes(key);
                desCryptoService.IV = Encoding.ASCII.GetBytes(iv);
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, desCryptoService.CreateEncryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(pText, 0, pText.Length);
                    cryptoStream.Close();
                    memoryStream.Close();
                    var result = Encoding.Default.GetString(memoryStream.ToArray());
                    return result;
                }
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <param name="key">Symmetric key that is used for encryption and decryption.</param>
        /// <param name="iv">Initialization vector (IV) for the symmetric algorithm.</param>
        /// <returns></returns>
        public static string Decrypt(string encryptedText, string key, string iv)
        {
            var encryptedTextByte = Encoding.Default.GetBytes(encryptedText); // parse text to bites array
            using (var desCryptoService = new DESCryptoServiceProvider())
            {
                desCryptoService.Key = Encoding.ASCII.GetBytes(key);
                desCryptoService.IV = Encoding.ASCII.GetBytes(iv);
                var decryptor = desCryptoService.CreateDecryptor(desCryptoService.Key, desCryptoService.IV);
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
