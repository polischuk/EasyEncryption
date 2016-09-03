using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EasyEncryption
{
    public abstract class SHA
    {
        public static string ComputeSHA1Hash(string stringToHash)
        {
            using (var sha1 = new SHA1Managed())
            {
                return
                    BitConverter.ToString(sha1.ComputeHash(Encoding.UTF8.GetBytes(stringToHash)))
                        .Replace("-", string.Empty)
                        .ToLower();
            }
        }
        public static string ComputeSHA256Hash(string stringToHash)
        {
            using (var hash = SHA256.Create())
            {
                return string.Join("", hash
                  .ComputeHash(Encoding.UTF8.GetBytes(stringToHash))
                  .Select(item => item.ToString("x2")));
            }
        }
    }
}
