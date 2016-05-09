using System.Collections.Generic;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace EasyEncryption.Tests.Algorithms
{
    [TestFixture]
    [Category("DES")]
    public class DesTests
    {
        [Test]
        [TestCaseSource(nameof(DesEncryptTestCases))]
        public void DesEncrypt_WithValidData_ShouldReturnEncryptedString(string text, string key, string iv)
        {
            var result = DES.Encrypt(text, key, iv);
            Assert.IsNotNull(result);
        }

        [Test]
        [TestCaseSource(nameof(DesEncryptTestCases))]
        public void DesDecrypt_WithValidData_ShouldReturnDecryptedString(string text, string key, string iv)
        {
            var encryptString = DES.Encrypt(text, key, iv);
            Assert.IsNotNull(encryptString);
            var result = DES.Decrypt(encryptString, key, iv);
            Assert.IsNotNull(result);
            Assert.AreEqual(text, result);
        }

        private static IEnumerable<TestCaseData> DesEncryptTestCases
        {
            get
            {
                var testCaseData = new List<TestCaseData>
                {
                    new TestCaseData("Test", "bZr2URKx", "HNtgQw0w"),
                    new TestCaseData("Polischuk", "iUaHmasV", "NFmYfOnW"),
                    new TestCaseData("Artem", "ghrw8z1m", "IGwRDzLA"),
                    new TestCaseData("JULIK", "IGwRDzLA", "r0BCQ6jv"),
                    new TestCaseData("Тестируем разную КІРїЛЁЦґ", "CQ6jvIGw", "Ar0BQ6jv")
                };
                return testCaseData;
            }
        }
    }
}

