using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EasyEncryption
{
    public abstract class MD5
    {
        /// <summary>
        /// Returns a MD5 hash as a string
        /// </summary>
        /// <param name="text">String to be hashed.</param>
        /// <returns>Hash as string.</returns>
        public static string ComputeMD5Hash(string text)
        {
            var encodedPassword = new UTF8Encoding().GetBytes(text);
            var hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
            var encoded = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
            return encoded;
        }
        public static bool IsValidMD5(string md5)
        {
            if (md5 == null || md5.Length != 32) return false;
            return md5.All(x => (x >= '0' && x <= '9') || (x >= 'a' && x <= 'f') || (x >= 'A' && x <= 'F'));
        }
    }
}
