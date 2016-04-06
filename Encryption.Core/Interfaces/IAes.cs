namespace Encryption.Framework.Interfaces
{
    /// <summary>
    /// Performs symmetric encryption and decryption using the Cryptographic Application Programming Interfaces (CAPI) implementation of the Advanced Encryption Standard (AES) algorithm.
    /// </summary>
    public interface IAes
    {
        /// <summary>
        /// Encrypt text using AES algorithm.
        /// </summary>
        /// <param name="plainText">The text that you want to encrypt</param>
        /// <param name="key">Symmetric key that is used for encryption and decryption.</param>
        /// <param name="iv">Initialization vector (IV) for the symmetric algorithm.</param>
        /// <returns>Encrypt text string</returns>
        string Encrypt(string plainText, string key, string iv);
        /// <summary>
        /// Decrypt text using AES algorithm.
        /// </summary>
        /// <param name="encryptedText">Your encrypted string</param>
        /// <param name="key">Symmetric key that is used for encryption and decryption.</param>
        /// <param name="iv">Initialization vector (IV) for the symmetric algorithm.</param>
        /// <returns>Decrypted string</returns>
        string Decrypt(string encryptedText,string key, string iv);
    }
}
