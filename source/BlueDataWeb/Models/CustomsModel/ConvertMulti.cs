using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueDataWeb.Models.CustomsModel
{
    public class ConvertMulti
    {
        public static DateTime? ConvertStringToDateTime_Split(string _str)
        {
            if (string.IsNullOrEmpty(_str)) return null;

            DateTime dtReturn = DateTime.Now;

            string[] arrDate = _str.Trim().Split('/');
            string year = arrDate[2];
            string month = arrDate[1];
            string day = arrDate[0];
            int hour = 0;
            int min = 0;
            int sec = 0;
            string[] arrHour;

            if (year.Contains(':'))
            {
                arrHour = year.Split(' ');
                year = arrHour[0];
                string strHour = arrHour[1];
                arrHour = strHour.Split(':');
                if (arrHour.Count() == 3)
                {
                    hour = Convert.ToInt16(arrHour[0]);
                    min = Convert.ToInt16(arrHour[1]);
                    sec = Convert.ToInt16(arrHour[2]);
                }
            }

            try
            {
                dtReturn = new DateTime(Convert.ToInt16(year), Convert.ToInt16(month), Convert.ToInt16(day));//, hour, min, sec);
            }
            catch
            {
                return null;
            }

            return dtReturn;
        }
    }
}