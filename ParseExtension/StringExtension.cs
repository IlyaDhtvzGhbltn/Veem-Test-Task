using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseExtension.String
{
    public static class StringExtension
    {
        public static void TryParseAny(this string str, out decimal result)
        {
            result = -1;
            if (str.Contains(','))
                str = str.Replace(',', '.');
            IFormatProvider formatProvider = new NumberFormatInfo() { NumberDecimalSeparator = "." };
            Decimal.TryParse(str, NumberStyles.Any, formatProvider, out result);
        }
    }
}
