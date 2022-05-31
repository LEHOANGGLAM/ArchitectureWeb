using BlueDataWeb.Models.Entites;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlueDataWeb.Models.CustomsModel
{
    public class BaoGiaModel
    {
        public long BaoGiaID { get; set; }
        public int AppID { get; set; }
        public int LoaiBaoGiaID { get; set; }

        [Required (ErrorMessage ="Bắt buột nhập")]    
        public string Ten { get; set; }

        [Required(ErrorMessage = "Bắt buột nhập")]

        public string MoTaNgan { get; set; }
        public string MoTa { get; set; }

        [Required(ErrorMessage = "Bắt buột nhập")]

        public string MetaTitle { get; set; }

        [Required(ErrorMessage = "Bắt buột nhập")]

        public string MetaKeyword { get; set; }

        [Required(ErrorMessage = "Bắt buột nhập")]

        public string MetaDescription { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string HinhAnh { get; set; }
        public string FileBaoGia { get; set; }
        public string DocumentFile { get; set; }
        public string Video { get; set; }
        [Required(ErrorMessage = "Bắt buột nhập")]
        public string TongGia { get; set; }


        [Required(ErrorMessage = "Bắt buột nhập")]

        public int? OrderBy { get; set; }




        public BaoGiaModel UpdateModelToView(BaoGia news)
        {
            BaoGiaModel newView = new BaoGiaModel();
            newView = new BaoGiaModel
            {
                BaoGiaID = news.BaoGiaID,
                LoaiBaoGiaID = news.LoaiBaoGiaID,
                AppID = news.AppID,
                Ten = news.Ten,
                MoTa = news.MoTa,
                MoTaNgan = news.MoTaNgan,
                MetaTitle = news.MetaTitle,
                MetaKeyword = news.MetaKeyword,
                MetaDescription = news.MetaDescription,
                IsDelete = news.IsDelete,
                HinhAnh = news.HinhAnh,
                FileBaoGia = news.FileBaoGia,
                DocumentFile = news.DocumentFile,
                Video = news.Video,
                TongGia = news.TongGia,


                OrderBy = news.OrderBy

            };

            return newView;
        }
    }

    
}