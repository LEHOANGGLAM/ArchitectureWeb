using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueDataWeb.Utility
{
    public class PagerInfo
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }

        public int PageCount
        {
            get
            {
                return (int)Math.Ceiling(1.0 * RowCount / PageSize);
            }
        }

        public int LastPageNo
        {
            get
            {
                return PageCount - 1;
            }
        }

        public void Navigate(int PageNo)
        {
            if (PageNo < 0)
            {
                this.PageNo = LastPageNo;
            }
            else if (PageNo > this.LastPageNo)
            {
                this.PageNo = 0;
            }
            else
            {
                this.PageNo = PageNo;
            }
        }

        public static PagerInfo Get(String id, int PageSize = 10)
        {
            PagerInfo pi = HttpContext.Current.Session[id] as PagerInfo;
            if (pi == null) // chua ton tai trong session
            {
                pi = new PagerInfo
                {
                    PageSize = PageSize
                };
                HttpContext.Current.Session[id] = pi;
            }
            return pi;
        }
    }
}