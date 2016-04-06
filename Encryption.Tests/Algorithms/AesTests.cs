
namespace Encryption.Tests.Algorithms
{
    using System.Collections.Generic;
    using Framework.Algorithms;
    using NUnit.Framework;
    using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

    [TestFixture]
    [Category("AES")]
    public class AesTests
    {
        private Aes _aes;

        [OneTimeSetUp]
        public void Init()
        {
            _aes = new Aes();
        }

        [Test]
        [TestCaseSource(nameof(AesEncryptTestCases))]
        public void AesEncryptTest(string text, string key, string iv)
        {
            var result = _aes.Encrypt(text, key, iv);
            Assert.IsNotNull(result);
        }

        [Test]
        [TestCaseSource(nameof(AesEncryptTestCases))]
        public void AesDecryptTest(string text, string key, string iv)
        {
            var encryptString = _aes.Encrypt(text, key, iv);
            Assert.IsNotNull(encryptString);
            var result = _aes.Decrypt(encryptString, key, iv);
            Assert.IsNotNull(result);
            Assert.AreEqual(text, result);
        }

        private static IEnumerable<TestCaseData> AesEncryptTestCases
        {
            get
            {
                var testCaseData = new List<TestCaseData>
                {
                    new TestCaseData("Test", "HNtgQw0wAbZrURKx", "VN53WuL2VkKaVTf5"),
                    new TestCaseData("Polischuk", "iUaHmasVRY6xDIsZ", "NFmYfOnWCLFUZ0qq"),
                    new TestCaseData("Artem", "ghrw8z1mA6kOMQrFnwxq4321", "IGwRDzLAr0BCQ6jv"),
                    new TestCaseData("JULIK", "IGwRDzLAr0BCQ6jvIGwRDzLAr0BCQ6jv", "IGwRDzLAr0BCQ6jv"),
                    new TestCaseData("Тестируем разную КІРїЛЁЦґ", "IGwRDzLAr0BCQ6jvIGwRDzLAr0BCQ6jv", "IGwRDzLAr0BCQ6jv")
                };
                return testCaseData;
            }
        }
    }
}

