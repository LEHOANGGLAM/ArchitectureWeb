using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueDataWeb.Core
{
    public partial class News
    {
        public int NewsID { get; set; }
        public Nullable<int> AppID { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> Views { get; set; }
        public Nullable<long> NewCategoriesID { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<bool> IsNew { get; set; }
        public Nullable<bool> IsNoiBat { get; set; }
        public Nullable<int> OrderBY { get; set; }
        public string CaseStudy_OverView { get; set; }
        public string CaseStudy_Main { get; set; }
        public string CaseStudy_Use { get; set; }
        public Nullable<bool> OnLyTinTuc { get; set; }
        public Nullable<bool> isCaseStudy { get; set; }
        public string KeyName { get; set; }

        public virtual NewCategory NewCategory { get; set; }
    }
}
