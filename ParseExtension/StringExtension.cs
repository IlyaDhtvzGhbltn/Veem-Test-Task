using System;
using System.Linq;
using System.Globalization;


namespace ParseExtension.String
{
    public static class StringExtension
    {
        public static void TryParseAny(this string str, out decimal result)
        {
            if (string.IsNullOrWhiteSpace(str))
                throw new ArgumentNullException();
            result = -1;
            if (str.Contains(','))
                str = str.Replace(',', '.');
            IFormatProvider formatProvider = new NumberFormatInfo() { NumberDecimalSeparator = "." };
            Decimal.TryParse(str, NumberStyles.Any, formatProvider, out result);
        }
    }
}
