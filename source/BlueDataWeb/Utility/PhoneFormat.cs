using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueDataWeb.Utility
{
    public class PhoneFormat
    {
        public static string PhoneNumber(string value)
        {
            value = new System.Text.RegularExpressions.Regex(@"\D")
                .Replace(value, string.Empty);
            value = value.TrimStart('1');
            if (value.Length == 7)
                return Convert.ToInt64(value).ToString("0### ###");
            if (value.Length == 10)
                return Convert.ToInt64(value).ToString("0### ### ###");
            if (value.Length > 10)
                return Convert.ToInt64(value)
                    .ToString("0### ### ### " + new String('#', (value.Length - 10)));
            return value;
        }
    }
}