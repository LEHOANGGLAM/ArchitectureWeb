using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace BlueDataWeb.Utility
{
    public static class HelperSEO
    {
        public static string ToSeoUrl(this string url, int id=0)
        {
            // ensure the the is url lowercase
            string encodedUrl = (url.Replace('.','-') ?? "").ToLower();

            // replace & with and
            encodedUrl = Regex.Replace(encodedUrl, @"\&+", "and");

            // remove characters
            encodedUrl = encodedUrl.Replace("'", "");

            // remove invalid characters
            //encodedUrl = Regex.Replace(encodedUrl, @"[^a-z0-9]", "-");

            encodedUrl = convertToUnSign3(encodedUrl).Replace(@" ", "-").Replace(",", "-").Replace(":", "-").Replace("/", "-").Replace("?", "");

          

            // trim leading & trailing characters
            if(id !=0)
                encodedUrl = encodedUrl.Trim('-') + "-" + (id==0?0:id) + ".html";
            else
                encodedUrl = encodedUrl.Trim('-');

            //Remove all non-word-char in filename
            encodedUrl = Regex.Replace(encodedUrl, @"\W+", "-");

            // remove duplicates
            encodedUrl = Regex.Replace(encodedUrl, @"-+", "-");
            
            return encodedUrl;
        }
        public static string ToSeoImage(this string url)
        {
            // ensure the the is url lowercase
            string encodedUrl = (url ?? "").ToLower();

            // replace & with and
            encodedUrl = Regex.Replace(encodedUrl, @"\&+", "and");

            // remove characters
            encodedUrl = encodedUrl.Replace("'", "");

            // remove invalid characters
            //encodedUrl = Regex.Replace(encodedUrl, @"[^a-z0-9]", "-");

            encodedUrl = convertToUnSign3(encodedUrl).Replace(@" ", "-");

            // remove duplicates
            encodedUrl = Regex.Replace(encodedUrl, @"-+", "-");

            // trim leading & trailing characters
            encodedUrl = encodedUrl.Trim('-');

            return encodedUrl;
        }
        public static string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        } 
    }
}