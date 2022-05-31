using BlueDataWeb.Areas.Admin.Models;
using BlueDataWeb.Models.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace BlueDataWeb.Models.CustomsModel
{

    public class NewsModels
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();

        public int NewsID { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        public int AppID { get; set; }
        [Required]
        [Display(Name = "ShortDescription")]
        public string ShortDescription { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

 
        [Display(Name = "Image")]
        public string ImagePath { get; set; }

        public Nullable<System.DateTime> CreatedDate { get; set; }

        [Required(ErrorMessage = "is required")]
        [StringLength(65, ErrorMessage = "Cannot be longer than 65 characters.")]
        public string MetaTitle { get; set; }


        [Required(ErrorMessage = "is required")]
        [StringLength(100, ErrorMessage = "Cannot be longer than 100 characters.")]
        public string MetaKeywords { get; set; }

        [Required(ErrorMessage = "is required")]
        [StringLength(160, ErrorMessage = "Cannot be longer than 160 characters.")]
        public string MetaDescription { get; set; }

        public Nullable<bool> IsNew { get; set; }

        public Nullable<bool> IsNoiBat { get; set; }

        public long NewCategoriesID { get; set; }

        public Nullable<int> Views { get; set; }

        public Nullable<bool> IsDelete { get; set; }


        public Nullable<int> OrderBY { get; set; }
        public string CaseStudy_OverView { get; set; }
        public string CaseStudy_Main { get; set; }
        public string CaseStudy_Use { get; set; }
         
        public bool? isCaseStudy { get; set; }
        public string KeyName { get; set; }

        public SelectList listCate { get; set; }

        public NewsModels( )
        {
            
        }
        public NewsModels ( string KeyName, int AppID, string NewCateIDSelected = ""  )
        {
            if (this.NewsID > 0)
            {
                this.AppID = AppID;
                this.KeyName = KeyName;
              
            }
            BinCommbox(KeyName, AppID, NewCateIDSelected);
        }

        public void BinCommbox(string KeyName, int AppID , string NewCateIDSelected )
        {

            var cate = db.NewCategories.Where(x => x.KeyName == KeyName && x.AppID == AppID).First();
            var lstCate = NewCateHelper.GetListChild(cate.NewCategoriesID, false);
            List<long> ListCateChild = lstCate.Select(x => x.NewCategoriesID).ToList();

            this.listCate = new SelectList(db.NewCategories.Where(x => ListCateChild.Contains(x.NewCategoriesID)), "NewCategoriesID", "Name", NewCateIDSelected);
        }
        
    }

    
}