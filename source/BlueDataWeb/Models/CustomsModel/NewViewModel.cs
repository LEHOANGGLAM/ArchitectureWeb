using BlueDataWeb.Models.Entites;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlueDataWeb.Models.CustomsModel
{
    public class NewViewModel
    {
        public int NewsID { get; set; }

     
     
        public string Title { get; set; }
       
   
        public string ShortDescription { get; set; }
         
        public string ImagePath { get; set; }
        public string NewCategoryName { get; set; }
        public DateTime? CreatedDate { get; set; }







    }

    
}