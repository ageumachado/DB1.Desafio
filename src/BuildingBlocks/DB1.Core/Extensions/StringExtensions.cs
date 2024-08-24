using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB1.Core.Extensions
{
    public static class StringExtensions
    {
        public static decimal ToDecimal(this string? value)
        {
            if (decimal.TryParse(value, out decimal result)) return result;

            return 0;
        }

        public static string ApenasNumeros(this string input)
            => new(input.Where(char.IsDigit).ToArray());
    }
}
