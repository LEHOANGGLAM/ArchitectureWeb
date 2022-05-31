using BlueDataWeb.Models.Entites;
using System.Collections.Generic;
using WebMVC.Entities;

namespace BlueDataWeb.Models.Enum
{
    public enum NewsType
    {
        E_ChucNang,
        E_DichVu,
        E_GiaiPhap,
        E_HuongDan,
        E_Outsourcing,
        E_SanPham,
        E_TemNhan,
        E_TinTuc,
        E_TrienKhai,
        E_UngDung
    }

    public enum ETrangThaiContact
    {
        E_DangKyMoi, E_DaCapPhat, E_DaLienHe
    }

    public enum EDichVu
    {
        E_Brandname, E_Hosting, E_2Ways, E_SMS
    }

    public enum ESlider
    {
        E_Home, E_GioiThieu, E_ChucNang, E_TinTuc, E_TrienKhai
    }

    public enum EMoTaDichVu
    {
        E_QC, E_API
    }

    public enum EGoiDichVu
    {
        E_Basic, E_Full
    }

    public enum ESex
    {
        E_Nam, E_Nu
    }

    public enum EPaymentTransaction
    {
        E_Transfer, E_Cash, E_Ship
    }

    public enum typeImg
    {
        E_Slider, E_Product
    }

    public class ItemStatus
    {
        public string ID { get; set; }
        public string Value { get; set; }
        public int Orderby { get; set; }
        public bool IsSelected { get; set; }
    }

    public class DataModel
    {
        #region Enum giới tính

        public List<ItemStatus> getsex()
        {
            return new List<ItemStatus>()
            {
                new ItemStatus(){ ID = ESex.E_Nam.ToString(), Value= "Nam"},
                new ItemStatus(){ ID = ESex.E_Nu.ToString(), Value= "Nữ"}
            };
        }

        #endregion Enum giới tính

        #region Enum Hình ảnh

        public List<ItemStatus> GettypeImg()
        {
            return new List<ItemStatus>()
            {
                new ItemStatus(){ ID = typeImg.E_Slider.ToString(), Value= "Slider"},
                new ItemStatus(){ ID = typeImg.E_Product.ToString(), Value= "Product"}
            };
        }

        public List<ItemStatus> getTrangThaiLienHe()
        {
            return new List<ItemStatus>()
            {
                new ItemStatus(){ ID = ETrangThaiContact.E_DangKyMoi.ToString(), Value= "Đăng ký mới"},
                new ItemStatus(){ ID = ETrangThaiContact.E_DaLienHe.ToString(), Value= "Đã liên hệ"},
                new ItemStatus(){ ID = ETrangThaiContact.E_DaCapPhat.ToString(), Value= "Đã cấp phát"}
            };
        }

        public List<ItemStatus> getDichVu()
        {
            return new List<ItemStatus>()
            {
                new ItemStatus(){ ID = EDichVu.E_Brandname.ToString(), Value= "SMS Brandname"},
                new ItemStatus(){ ID = EDichVu.E_Hosting.ToString(), Value= "SMS Hosting"},
                new ItemStatus(){ ID = EDichVu.E_2Ways.ToString(), Value= "SMS Hai Chiều"},
                new ItemStatus(){ ID = EDichVu.E_SMS.ToString(), Value= "Đầu số SMS"}
            };
        }

        public List<ItemStatus> getMoTaDichVu()
        {
            return new List<ItemStatus>()
            {
                new ItemStatus(){ ID = EMoTaDichVu.E_QC.ToString(), Value= "Chỉ dùng SMS để quảng cáo và chăm sóc khách hàng"},
                new ItemStatus(){ ID = EMoTaDichVu.E_API.ToString(), Value= "Tích hợp API SMS vào phầm mềm, website..."}
            };
        }

        public List<ItemStatus> getGoiDichVu()
        {
            return new List<ItemStatus>()
            {
                new ItemStatus(){ ID = EGoiDichVu.E_Basic.ToString(), Value= "Triển khai bảo hành điện tử đơn giản"},
                new ItemStatus(){ ID = EGoiDichVu.E_Full.ToString(), Value= "Triển khai đầy đủ các chức năng"}
            };
        }

        public List<ItemStatus> getBangGia(int AppID)
        {
            var datareturn = new List<ItemStatus>();
            var srv = new BaoGiaService();
            List<BaoGia> dataBaoGia = new List<BaoGia>();
            dataBaoGia = srv.BaoGia_GetAll(AppID, 1);

            foreach (var item in dataBaoGia)
            {
                var temp = new ItemStatus() { ID = item.BaoGiaID.ToString(), Value = item.Ten.ToString() + " - " + item.TongGia };

                datareturn.Add(temp);
            }

            return datareturn;
        }

        #endregion Enum Hình ảnh

        #region Enum Slider

        public List<ItemStatus> GetTypeSlider()
        {
            return new List<ItemStatus>()
            {
                new ItemStatus(){ ID = ESlider.E_Home.ToString(), Value= "Trang chủ"},
                new ItemStatus(){ ID = ESlider.E_GioiThieu.ToString(), Value= "Giới thiệu"},
                new ItemStatus(){ ID = ESlider.E_ChucNang.ToString(), Value= "Chức năng"},
                new ItemStatus(){ ID = ESlider.E_TinTuc.ToString(), Value= "Tin tức"},
                new ItemStatus(){ ID = ESlider.E_TrienKhai.ToString(), Value= "Triển khai"},
            };
        }

        #endregion Enum Slider
    }
}