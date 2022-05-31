using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

 
    public static class HelperSEOName
    {

        public static string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }


        public static string ToSeoUrl(this string url)
        {
            // make the url lowercase
            // ensure the the is url lowercase
            string encodedUrl = convertToUnSign3(url).ToLower();
            encodedUrl = Regex.Replace(encodedUrl, @"\W+", "-");
            return encodedUrl;
        }

    }
 