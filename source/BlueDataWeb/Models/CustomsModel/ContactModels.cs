using BlueDataWeb.Models.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace BlueDataWeb.Models.CustomsModel
{
    public class ContactModels
    {
        [Required(ErrorMessage = "Hãy nhập họ tên")]
        public string Name { get; set; }

        public int AppID { get; set; }

        [Required(ErrorMessage = "Hãy nhập email")]
        [EmailAddress(ErrorMessage = "Email chưa đúng định dạng")]
        public string Email { get; set; }

        public string contents { get; set; }

        [Required(ErrorMessage = "Hãy nhập số điện thoại")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Số điện thoại chưa đúng định dạng")]

        public string Phone { get; set; }
 
        public string DichVu { get; set; }

        public string MoTaDichVu { get; set; }

        [Required(ErrorMessage = "Hãy nhập tên công ty")]
        public string CongTy { get; set; }

        [Required(ErrorMessage = "Hãy nhập lĩnh vực")]
        public string LinhVuc { get; set; }

        [Required(ErrorMessage = "Hãy nhập chức vụ")]
        public string ChucVu { get; set; }

        [Required(ErrorMessage = "Hãy nhập địa chỉ")]
        public string Address { get; set; }

        public List<ItemStatus> lstDichVu { get; set; }
        public List<ItemStatus> lstMotaDichVu { get; set; }
        public List<SelectListItem> lstGoiDichVu { get; set; }
        public List<SelectListItem> lstBangGia { get; set; }
        public long BangGiaID { get;   set; }

        public ContactModels()
        {
            lstDichVu = new DataModel().getDichVu();
            lstMotaDichVu = new DataModel().getMoTaDichVu();

            int appID = int.Parse( System.Configuration.ConfigurationManager.AppSettings["AppID"].ToString());
            var  _lstBangGia = new DataModel().getBangGia(appID);
            lstBangGia = _lstBangGia.Select(x => new SelectListItem { Text = x.Value, Value = x.ID.ToString() }).ToList();


            List<ItemStatus> lst = new DataModel().getGoiDichVu();
            
            lstGoiDichVu = lst.Select(x => new SelectListItem { Text = x.Value, Value = x.ID.ToString() }).ToList();
        }
    }
}