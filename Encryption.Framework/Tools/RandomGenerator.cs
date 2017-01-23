using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyEncryption.Enums;

namespace EasyEncryption.Tools
{
    public abstract class RandomGenerator
    {
        /// <summary>
        /// Generate random number
        /// </summary>
        /// <param name="numberLength">number length</param>
        /// <returns></returns>
        public static string GenerateNumber(int numberLength)
        {
            if (numberLength < 0)
            {
                throw new ArgumentOutOfRangeException("numberLength must be >= 0");
            }
            var random = new Random();
            var output = new StringBuilder();
            for (var i = 0; i < numberLength; i++)
            {
                output.Append(random.Next(0, 10));
            }
            return output.ToString();
        }
    }
}
