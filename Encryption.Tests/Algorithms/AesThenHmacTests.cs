using System;
using System.Collections.Generic;
using System.Text;
using Encryption.Framework.Algorithms;
using NUnit.Framework;

namespace Encryption.Tests.Algorithms
{
    [TestFixture]
    [Category("AES")]
    public class AesThenHmacTests
    {
        #region SimpleEncryptWithPassword
        [Test]
        [TestCaseSource(nameof(SimpleEncryptWithPasswordTestCases))]
        public void SimpleEncryptWithPassword_WithValidData_ShouldReturnEncryptedString(string message, string password)
        {
            var result = AesThenHmac.SimpleEncryptWithPassword(message, password);
            Assert.IsNotNull(result);
        }
        [Test]
        [TestCaseSource(nameof(SimpleEncryptWithPasswordTestCases))]
        public void SimpleEncryptWithPassword_WithValidData_ShouldReturnEncryptedBytes(string message, string password)
        {
            var bytesMessage = Encoding.Default.GetBytes(message);
            var result = AesThenHmac.SimpleEncryptWithPassword(bytesMessage, password);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
        }

        [Test]
        [TestCase("Artem", "")]
        [TestCase("Polischuk", null)]
        public void SimpleEncryptWithPassword_WithInvalidPassword_ShouldReturnArgumentException(string message, string password)
        {
            try
            {
                AesThenHmac.SimpleEncryptWithPassword(message, password);
                Assert.Fail("Password not validated");
            }
            catch (ArgumentException ex)
            {
                Assert.IsNotNull(ex);
            }
        }
        #endregion

        #region SimpleDecryptWithPassword
        [Test]
        [TestCaseSource(nameof(SimpleEncryptWithPasswordTestCases))]
        public void SimpleDecryptWithPassword_WithValidData_ShouldReturnEncryptedString(string message, string password)
        {
            var encryptString = AesThenHmac.SimpleEncryptWithPassword(message, password);
            var result = AesThenHmac.SimpleDecryptWithPassword(encryptString, password);
            Assert.IsNotNull(result);
            Assert.AreEqual(message, result);
        }
        [Test]
        [TestCaseSource(nameof(SimpleEncryptWithPasswordTestCases))]
        public void SimpleDecryptWithPassword_WithValidData_ShouldReturnEncryptedBytes(string message, string password)
        {
            var bytesMessage = Encoding.Default.GetBytes(message);
            var encryptByte = AesThenHmac.SimpleEncryptWithPassword(bytesMessage, password);
            var result = AesThenHmac.SimpleDecryptWithPassword(encryptByte, password);
            Assert.IsNotNull(result);
            var stringResult = Encoding.Default.GetString(result);
            Assert.AreEqual(message, stringResult);
        }

        [Test]
        [TestCase("Artem", "")]
        [TestCase("Polischuk", null)]
        public void SimpleDecryptWithPassword_WithInvalidPassword_ShouldReturnArgumentException(string message, string password)
        {
            try
            {
                AesThenHmac.SimpleDecryptWithPassword(message, password);
                Assert.Fail("Password not validated");
            }
            catch (ArgumentException ex)
            {
                Assert.IsNotNull(ex);
            }
        }
        #endregion

        #region SimpleEncrypt
        [Test]
        [TestCaseSource(nameof(SimpleEncryptTestCases))]
        public void SimpleEncrypt_WithValidData_ShouldReturnEncryptedString(byte[] message, byte[] password, byte[] authKey)
        {
            var result = AesThenHmac.SimpleEncrypt(message, password, authKey);
            Assert.IsNotNull(result);
        }

        public void SimpleEncrypt_WithInvalidCryptKeyLength_ShouldReturnArgumentException()
        {
            try
            {
                AesThenHmac.SimpleEncrypt(new byte[15], new byte[4], new byte[32]);
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.IsNotNull(ex);
                Assert.AreEqual(ex.Message, "Key needs to be 256 bit!\r\nParameter name: cryptKey");
            }
        }
        [Test]
        public void SimpleEncrypt_WithInvalidAuthKeyLength_ShouldReturnArgumentException()
        {
            try
            {
                AesThenHmac.SimpleEncrypt(new byte[15], new byte[32], new byte[45]);
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.IsNotNull(ex);
                Assert.AreEqual(ex.Message, "Key needs to be 256 bit!\r\nParameter name: authKey");
            }
        }
        #endregion

        #region SimpleDecrypt
        [Test]
        [TestCaseSource(nameof(SimpleEncryptTestCases))]
        public void SimpleDecrypt_WithValidData_ShouldReturnEncryptedBytes(byte[] message, byte[] password, byte[] authKey)
        {
            var encryptByte = AesThenHmac.SimpleEncrypt(message, password, authKey);
            var result = AesThenHmac.SimpleDecrypt(encryptByte, password, authKey);
            Assert.IsNotNull(result);
            Assert.AreEqual(message, result);
        }

        [Test]
        public void SimpleDecrypt_WithInvalidCryptKeyLength_ShouldReturnArgumentException()
        {
            try
            {
                var encryptByte = AesThenHmac.SimpleEncrypt(new byte[15], new byte[32], new byte[32]);
                AesThenHmac.SimpleDecrypt(encryptByte, new byte[15], new byte[15]);
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.IsNotNull(ex);
                Assert.AreEqual(ex.Message, "CryptKey needs to be 256 bit!\r\nParameter name: cryptKey");
            }
        }
        [Test]
        public void SimpleDecrypt_WithInvalidAuthKeyLength_ShouldReturnArgumentException()
        {
            try
            {
                var encryptByte = AesThenHmac.SimpleEncrypt(new byte[15], new byte[32], new byte[32]);
                AesThenHmac.SimpleDecrypt(encryptByte, new byte[32], new byte[15]);
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.IsNotNull(ex);
                Assert.AreEqual(ex.Message, "AuthKey needs to be 256 bit!\r\nParameter name: authKey");
            }
        }
        #endregion

        #region TestCases
        private static IEnumerable<TestCaseData> SimpleEncryptTestCases
        {
            get
            {
                var testCaseData = new List<TestCaseData>
                {
                    new TestCaseData(Encoding.UTF8.GetBytes("Test"),
                        Encoding.UTF8.GetBytes("IGwRDzLAr0BCQ6jvIGwRDzLAr0BCQ6jv"),
                        Encoding.UTF8.GetBytes("IGwRDzLAr0BCQ6jvIGwRDzLAr0BCQ6jv")),
                    new TestCaseData(Encoding.UTF8.GetBytes("Polischuk"),
                        Encoding.UTF8.GetBytes("IGwRDzLAr0BCQ6jvIGwRDzLAr0BCQ6jv"),
                        Encoding.UTF8.GetBytes("IGwRDzLAr0BCQ6jvIGwRDzLAr0BCQ6jv")),
                    new TestCaseData(Encoding.UTF8.GetBytes("Artem"),
                        Encoding.UTF8.GetBytes("IGwRDzLAr0BCQ6jvIGwRDzLAr0BCQ6jv"),
                        Encoding.UTF8.GetBytes("IGwRDzLAr0BCQ6jvIGwRDzLAr0BCQ6jv")),
                    new TestCaseData(Encoding.UTF8.GetBytes("Тестируем разную КІРїЛЁЦґ"),
                        Encoding.UTF8.GetBytes("IGwRDzLAr0BCQ6jvIGwRDzLAr0BCQ6jv"),
                        Encoding.UTF8.GetBytes("IGwRDzLAr0BCQ6jvIGwRDzLAr0BCQ6jv"))
                };
                return testCaseData;
            }
        }
        private static IEnumerable<TestCaseData> SimpleEncryptWithPasswordTestCases
        {
            get
            {
                var testCaseData = new List<TestCaseData>
                {
                    new TestCaseData("Test", "HNtgQw0wAbZrURKx"),
                    new TestCaseData("Polischuk", "iUaHmasVRY6xDIsZ"),
                    new TestCaseData("Artem", "ghrw8z1mA6kOMQrFnwxq4321"),
                    new TestCaseData("JULIK", "IGwRDzLAr0BCQ6jvIGwRDzLAr0BCQ6jv"),
                    new TestCaseData("Тестируем разную КІРїЛЁЦґ", "IGwRDzLAr0BCQ6jvIGwRDzLAr0BCQ6jv"),
                    new TestCaseData("JULIK", "1234"),
                    new TestCaseData("JULIK", "12345"),
                    new TestCaseData("JULIK", "123")
                };
                return testCaseData;
            }
        }
        #endregion
    }
}
