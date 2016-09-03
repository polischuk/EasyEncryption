using System.Text;
using NUnit.Framework;

namespace EasyEncryption.Tests.Algorithms
{
    [TestFixture]
    [Category("RSA")]
    public class RSATests
    {
        [Test]
        public void GenerateRSAKeys()
        {
            var keys = RSA.GenerateKeys(2048);
            Assert.IsNotNull(keys);
            Assert.IsNotNull(keys.PublicKey);
            Assert.IsNotNull(keys.PrivateKey);
        }
        [Test]
        public void Encrypt_WithValidData_ShouldReturnEncryptedBytes()
        {
            var keys = RSA.GenerateKeys(2048);
            var encryptedData = RSA.Encrypt(Encoding.UTF8.GetBytes("test"), keys.PrivateKey);
            Assert.IsNotNull(encryptedData);
        }
        [Test]
        public void Decrypt_WithValidData_ShouldReturnDecryptedBytes()
        {
            var keys = RSA.GenerateKeys(2048);
            var data = Encoding.UTF8.GetBytes("test");
            var encryptedData = RSA.Encrypt(data, keys.PublicKey);
            Assert.IsNotNull(encryptedData);
            var decryptedData = RSA.Decrypt(encryptedData, keys.PrivateKey);
            Assert.AreEqual(data, decryptedData);
        }
        [Test]
        public void Encrypt_WithValidData_ShouldReturnEncryptedString()
        {
            var keys = RSA.GenerateKeys(2048);
            var encryptedData = RSA.Encrypt("test", keys.PrivateKey);
            Assert.IsNotNull(encryptedData);
        }
        [Test]
        public void Decrypt_WithValidData_ShouldReturnDecryptedString()
        {
            var keys = RSA.GenerateKeys(2048);
            var data = "test";
            var encryptedData = RSA.Encrypt(data, keys.PublicKey);
            Assert.IsNotNull(encryptedData);
            var decryptedData = RSA.Decrypt(encryptedData, keys.PrivateKey);
            Assert.AreEqual(data, decryptedData);
        }
        [Test]
        public void SignData_WithValidData_ShouldReturnSignString()
        {
            var keys = RSA.GenerateKeys(2048);
            var encryptedData = RSA.Encrypt("test", keys.PrivateKey);
            Assert.IsNotNull(encryptedData);
        }
        [Test]
        public void VerifyData_WithValidData_ShouldReturnTrue()
        {
            var keys = RSA.GenerateKeys(2048);
            var data = "test";
            var signData = RSA.SignData(data, keys.PrivateKey);
            Assert.IsNotNull(signData);
            var isCorrect = RSA.VerifyData(data, signData, keys.PublicKey);
            Assert.IsTrue(isCorrect);
        }
    }
}
