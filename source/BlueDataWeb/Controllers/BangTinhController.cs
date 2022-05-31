using System.Web.Mvc;

namespace BlueDataWeb.Controllers
{
    public class BangTinhController : Controller
    {
        //
        // GET: /BangTinh/

        [HttpPost]
        public JsonResult Tinh(BangTinhRequest model)
        {
            BangTinhResult result = new BangTinhResult();

            result.DTTangTret = model.ChieuDai * model.ChieuRong;

            switch (model.LoaiMong)
            {
                case "Mong_Dai":
                    {
                        result.Mong = result.DTTangTret / 2;
                    }
                    break;

                case "Mong_Bang":
                    {
                        result.Mong = result.DTTangTret * 60 / 100;
                    }
                    break;

                case "Mong_Don":
                    {
                        result.Mong = result.DTTangTret * 30 / 100;
                    }
                    break;
            }

            switch (model.LoaiMai)
            {
                case "MaiNgoi":
                    {
                        result.Mong = result.DTTangTret;
                    }
                    break;

                case "MaiBTCT":
                    {
                        result.Mong = result.DTTangTret * 30 / 100;
                    }
                    break;

                case "MaiTonLanh":
                    {
                        result.Mong = result.DTTangTret * 30 / 100;
                    }
                    break;
            }

            result.TongDienTich = (result.DTTangTret * model.SoLau) + result.Mai + result.Mong;
            if (model.HinhThuc == "XD_Tho")
                result.DonGiaGoc = 3200000;
            if (model.HinhThuc == "XD_TronGoi")
                result.DonGiaGoc = 5000000;

            switch (model.LoaiNha)
            {
                case "NhaPho":
                    {
                        result.LoaiNha = "Nhà phố";
                    }
                    break;

                case "Cap4":
                    {
                        result.LoaiNha = "Nhà cấp 4";
                    }
                    break;

                case "BietThu":
                    {
                        result.LoaiNha = "Biệt thự";
                    }
                    break;
            }

            if (result.DonGiaGoc > 0)
            {
                var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
                result.DonGia = string.Format(info, "{0:c}", result.DonGiaGoc);
            }

            var tongtien = result.TongDienTich * result.DonGiaGoc;
            if (tongtien > 0)
            {
                var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
                result.TongSoTien = string.Format(info, "{0:c}", tongtien);
            }

           
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }

    public class BangTinhRequest
    {
        public string LoaiNha { get; set; }
        public string HinhThuc { get; set; }
        public int ChieuDai { get; set; }
        public int ChieuRong { get; set; }
        public string LoaiMong { get; set; }
        public string LoaiMai { get; set; }
        public int SoLau { get; set; }
    }

    public class BangTinhResult
    {
        public decimal DTTangTret { get; set; }
        public decimal Mong { get; set; }

        public decimal Mai { get; set; }

        public decimal TongDienTich { get; set; }
        public decimal DonGiaGoc { get; set; }
        public decimal DonGiaTinh{ get; set; }
        public string DonGia { get; set; }
        public string TongSoTien { get; set; }
        public string LoaiNha { get; set; }

        public BangTinhResult()
        {
            DTTangTret = 0;
            Mong = 0;
            Mai = 0;
            TongDienTich = 0;
            DonGiaGoc = 0;
            DonGiaTinh = 0;
           
            TongSoTien = "";
            LoaiNha = "";
        }
    }
}