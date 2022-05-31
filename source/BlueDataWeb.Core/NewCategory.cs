using System;
using System.Collections.Generic;

namespace BlueDataWeb.Core
{
    public partial class NewCategory
    {
        public NewCategory()
        {
            this.News = new HashSet<News>();
        }

        public long NewCategoriesID { get; set; }
        public Nullable<int> AppID { get; set; }
        public string Name { get; set; }
        public Nullable<bool> IsShow { get; set; }
        public string Description { get; set; }
        public Nullable<int> ParentCategoryID { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string Class { get; set; }
        public string Image { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public string KeyName { get; set; }
        public string DescriptionContent { get; set; }

        public virtual ICollection<News> News { get; set; }
    }
}