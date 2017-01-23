using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyEncryption.Enums;
using EasyEncryption.Tools;
using NUnit.Framework;

namespace EasyEncryption.Tests.Tools
{
    [TestFixture]
    [Category("RandomGenerator")]
    public class RandomGeneratorTests
    {
        [Test]
        [TestCaseSource(nameof(GenerateNumberTestCases))]
        public void GenerateNumber_WithCorrectNumberLength_ShouldReturnRandomNumberString(int numberLength)
        {
            var result = RandomGenerator.GenerateNumber(numberLength);
            Assert.IsNotNull(result);
            Assert.AreEqual(numberLength, result.Length);
        }

        [Test]
        public void GenerateNumber_NumberLengthLessThan0_ShouldReturnArgumentOutOfRangeException()
        {
            try
            {
                RandomGenerator.GenerateNumber(-1);
            }
            catch (ArgumentOutOfRangeException)
            {
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        private static IEnumerable<TestCaseData> GenerateNumberTestCases
        {
            get
            {
                var testCaseData = new List<TestCaseData>
                {
                    new TestCaseData(1),
                    new TestCaseData(2),
                    new TestCaseData(3),
                    new TestCaseData(4),
                    new TestCaseData(5),
                    new TestCaseData(6),
                    new TestCaseData(7),
                    new TestCaseData(15)
                };
                return testCaseData;
            }
        }
    }
}