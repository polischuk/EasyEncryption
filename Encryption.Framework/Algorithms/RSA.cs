using System;
using System.Security.Cryptography;
using System.Text;

namespace EasyEncryption
{
    public abstract class RSA
    {
        public static RSAKeysModel GenerateKeys(int keySize)
        {
            using (var rsa = new RSACryptoServiceProvider(keySize))
            {
                var model = new RSAKeysModel
                {
                    PrivateKey = rsa.ToXmlString(true),
                    PublicKey = rsa.ToXmlString(false)
                };
                return model;
            }

        }

        #region Encryption
        /// <summary>
        /// Encrypt data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] data, string publicKey)
        {
            using (var rsaCryptoServiceProvider = new RSACryptoServiceProvider())
            {
                rsaCryptoServiceProvider.FromXmlString(publicKey);
                var encryptedData = rsaCryptoServiceProvider.Encrypt(data, false);
                return encryptedData;
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static string Encrypt(string data, string publicKey)
        {
            var dataArray = Encoding.UTF8.GetBytes(data);
            var res = Convert.ToBase64String(Encrypt(dataArray, publicKey));
            return res;
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <param name="privateKey"></param>
        /// <returns>Base64 string result</returns>
        public static byte[] Decrypt(byte[] data, string privateKey)
        {
            using (var rsaCryptoServiceProvider = new RSACryptoServiceProvider())
            {
                rsaCryptoServiceProvider.FromXmlString(privateKey);
                var decryptedData = rsaCryptoServiceProvider.Decrypt(data, false);
                return decryptedData;
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string Decrypt(string data, string privateKey)
        {
            var dataArray = Convert.FromBase64String(data);
            var res = Encoding.UTF8.GetString(Decrypt(dataArray, privateKey));
            return res;
        }

        #endregion

        #region Sign
        public static string SignData(string message, string privateKey)
        {
            //// The array to store the signed message in bytes
            byte[] signedBytes;
            using (var rsa = new RSACryptoServiceProvider())
            {
                var encoder = new UTF8Encoding();
                var originalData = encoder.GetBytes(message);

                try
                {
                    rsa.FromXmlString(privateKey);
                    // Sign the data, using SHA512 as the hashing algorithm
                    signedBytes = rsa.SignData(originalData, CryptoConfig.MapNameToOID("SHA512"));
                }
                catch (CryptographicException e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
            //// Convert the a base64 string before returning
            return Convert.ToBase64String(signedBytes);
        }

        public static bool VerifyData(string originalMessage, string signedMessage, string publicKey)
        {

            using (var rsa = new RSACryptoServiceProvider())
            {
                var bytesToVerify = Encoding.UTF8.GetBytes(originalMessage);
                var signedBytes = Convert.FromBase64String(signedMessage);
                rsa.FromXmlString(publicKey);
                return rsa.VerifyData(bytesToVerify, CryptoConfig.MapNameToOID("SHA512"), signedBytes);
            }
        }
        #endregion
    }

    public class RSAKeysModel
    {
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
    }

}
