using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BlueDataWeb.Models.CustomsModel
{
    public class ArchitectureNewsDto
    {
        public long ID { get; set; }

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

        public long CategoriesID { get; set; }

        public Nullable<int> Views { get; set; }

        public Nullable<bool> IsDelete { get; set; }

        public Nullable<int> OrderBY { get; set; }
        
        public string KeyName { get; set; }
        public List<SelectListItem> Catelst { get; set; }

     
        
    }
}