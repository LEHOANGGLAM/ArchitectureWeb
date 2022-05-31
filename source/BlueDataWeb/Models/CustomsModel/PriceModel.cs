using BlueDataWeb.Models.Entites;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlueDataWeb.Models.CustomsModel
{
    public partial class JSonDoDaiViTri_Price
    {
        public string TenChucNang { get; set; }
        public Nullable<double> GiaTien { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }

    public partial class PriceModel
    {
        public long Id { get; set; }

        public int AppID { get; set; }

        [Required]

        public string Description { get; set; }

        [Required]
        public string Name { get; set; }

        

        public string JSonChucNang { get; set; }


        public bool Active { get; set; }

        public double PriceChucNang { get; set; }

        

        public List<JSonDoDaiViTri_Price> LstJSonChucNang { get; set; }

        public PriceModel()
        {
            LstJSonChucNang = new List<JSonDoDaiViTri_Price>();
        }

        public void Map_From_Price(Price data)
        {
            Id = data.Id;
            AppID = data.AppID.Value;
            Description = data.Description;
            PriceChucNang = data.PriceChucNang.Value;
            Active = data.Active.Value;
            Name = data.Name;
            JSonChucNang = data.JSonChucNang;
            if (data.JSonChucNang !=null)
            {
                LstJSonChucNang = JsonConvert.DeserializeObject<List<JSonDoDaiViTri_Price>>(data.JSonChucNang);

            }
        }

        public Price Map_To_Price()
        {
            Price dataReturn = new Price();
            dataReturn.Id = Id;
            dataReturn.AppID = AppID;
            dataReturn.Description = Description;
            dataReturn.PriceChucNang = PriceChucNang;
            dataReturn.Active =  Active;
            dataReturn.Name =  Name;
       
            return dataReturn;
        }
    }
}