using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Encryption.Framework.Algorithms
{
    public class Des
    {
        public static string Encrypt(string text, string key, string iv, string filepath)
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
        public static string Decrypt(string encryptedText,string key, string iv)
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
