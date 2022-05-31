using BlueDataWeb.Models.Entites;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlueDataWeb.Models.CustomsModel
{
    public class AboutModels
    {
        public int ID { get; set; }
        public int AppID { get; set; }

        [Required(ErrorMessage = "Bắt đầu nhập")]
        public string Name { get; set; }

        public string NameAlias { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Bắt đầu nhập")]
        public string ShortDescription { get; set; }

        public string ImagePage { get; set; }
        public string ImagePath { get; set; }

        [Required(ErrorMessage = "Bắt đầu nhập")]
        [StringLength(65, ErrorMessage = "Cannot be longer than 65 characters.")]
        public string MetaTitle { get; set; }

        [Required(ErrorMessage = "Bắt đầu nhập")]
        [StringLength(100, ErrorMessage = "Cannot be longer than 100 characters.")]
        public string MetaKeywords { get; set; }

        [Required(ErrorMessage = "Bắt đầu nhập")]
        [StringLength(160, ErrorMessage = "Cannot be longer than 160 characters.")]
        public string MetaDescription { get; set; }

        public string LanguageId { get; set; }
        public Nullable<System.DateTime> InsertDate { get; set; }
        public Nullable<int> InsertbyUserId { get; set; }
        public Nullable<System.DateTime> LastUpdate { get; set; }
        public Nullable<int> UpdatebyUserId { get; set; }
        public string UserNameInsert { get; set; }
        public string UserNameUpdate { get; set; }

        [Required(ErrorMessage = "Bắt đầu nhập")]
        public Int32 Order_by { get; set; }

        public AboutModels UpdateModelToView(About news)
        {
            AboutModels newView = new AboutModels();
            newView = new AboutModels
            {
                ID = news.ID,
                AppID = news.AppID,
                Name = news.Name,
                NameAlias = news.NameAlias,
                Description = news.Description,
                ShortDescription = news.ShortDescription,
                ImagePage = news.ImagePage,
                ImagePath = news.ImagePath,
                MetaTitle = news.MetaTitle,
                MetaKeywords = news.MetaKeywords,
                MetaDescription = news.MetaDescription,
                LanguageId = news.LanguageId,
                InsertDate = news.InsertDate,
                InsertbyUserId = news.InsertbyUserId,
                LastUpdate = news.LastUpdate,
                UpdatebyUserId = UpdatebyUserId,
                UserNameInsert = UserNameInsert,
                UserNameUpdate = UserNameUpdate ,
                Order_by = news.Order_by.Value

            };

            return newView;
        }
    }
}