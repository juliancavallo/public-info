using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PublicInfo.Domain.Helpers
{
    public class StringHelper
    {
        public static string Normalize(string text)
        {
            string normalizedText = text.Normalize(NormalizationForm.FormD);
            Regex reg = new Regex("[^a-zA-Z0-9]");
            var result = reg.Replace(normalizedText, "").ToLower();
            return result;
        }
    }
}
