using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicInfo.Domain.Helpers
{
    public class CurrencyHelper
    {
        public static string ParseCurrencyValueToString(string value)
        {
            value = value.Replace('.', ',');
            var valueWithoutDecimals = decimal.Parse(value.Split(',')[0]);
            var valueOnCurrencyFormat = valueWithoutDecimals.ToString("C", CultureInfo.GetCultureInfo("es-AR"));
            
            return valueOnCurrencyFormat.TrimEnd('0').Trim(',');
        }
    }
}
