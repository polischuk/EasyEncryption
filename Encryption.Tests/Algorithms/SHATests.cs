using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace EasyEncryption.Tests.Algorithms
{
    [TestFixture]
    [Category("SHA")]
    public class SHATests
    {

        [Test]
        [TestCaseSource(nameof(ComputeHashTestCases))]
        public void ComputeSHA1Hash_WithValidData_ShouldReturnSHAString(string text)
        {
            var result = EasyEncryption.SHA.ComputeSHA1Hash(text);
            Assert.IsNotNull(result);
            Console.WriteLine(result);
        }

        [Test]
        [TestCaseSource(nameof(ComputeHashTestCases))]
        public void ComputeSHA256Hash_WithValidData_ShouldReturnSHAString(string text)
        {
            var result = EasyEncryption.SHA.ComputeSHA256Hash(text);
            Assert.IsNotNull(result);
            Console.WriteLine(result);
        }
        private static IEnumerable<TestCaseData> ComputeHashTestCases
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
