using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Encryption.Tests.Algorithms
{
    [TestFixture]
    [Category("MD5")]
    class MD5Tests
    {

        #region CalculateMD5Hash
        [Test]
        [TestCaseSource(nameof(CalculateMD5HashTestCases))]
        public void CalculateMD5Hash_WithValidData_ShouldReturnMD5String(string text)
        {
            var result = Encryption.Framework.Algorithms.MD5.CalculateMD5Hash(text);
            Assert.IsNotNull(result);
            Console.WriteLine(result);
        }
        #endregion

        #region IsValidMD5
        [Test]
        [TestCaseSource(nameof(CalculateMD5HashTestCases))]
        public void IsValidMD5_WithInvalidMD5_ShouldReturnFalse(string text)
        {
            var md5Hash = "012345";
            var result = Encryption.Framework.Algorithms.MD5.IsValidMD5(md5Hash);
            Assert.False(result);
        }

        [Test]
        [TestCaseSource(nameof(CalculateMD5HashTestCases))]
        public void IsValidMD5_WithValidMD5_ShouldReturnTrue(string text)
        {
            var md5Hash = Encryption.Framework.Algorithms.MD5.CalculateMD5Hash(text);
            Assert.IsNotNull(md5Hash);
            var result = Encryption.Framework.Algorithms.MD5.IsValidMD5(md5Hash);
            Assert.True(result);
        }
        #endregion

        private static IEnumerable<TestCaseData> CalculateMD5HashTestCases
        {
            get
            {
                var testCaseData = new List<TestCaseData>
                {
                    new TestCaseData("Test"),
                    new TestCaseData("Polischuk"),
                    new TestCaseData("Artem"),
                    new TestCaseData("JULIK"),
                    new TestCaseData("Тестируем разную КІРїЛЁЦґ")
                };
                return testCaseData;
            }
        }
    }
}
