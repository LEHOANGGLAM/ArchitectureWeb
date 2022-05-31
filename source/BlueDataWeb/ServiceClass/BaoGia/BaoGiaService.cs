using BlueDataWeb.Models.Entites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;

namespace WebMVC.Entities
{
    public class BaoGiaService
    {
        #region Loại báo giá

        public List<BaoGia_Loai> BaoGia_Loai_GetAll(int _AppID)
        {
            using (var context = new BlueDataWebEntities())
            {
                List<BaoGia_Loai> data = context.BaoGia_Loai.Where(x => x.IsDelete == false && x.AppID == _AppID).OrderByDescending(x => x.ID).ToList();

                return data;
            }
        }

        #endregion Loại báo giá

        #region Thuộc tính Loại báo giá

        public List<BaoGia_Loai_ThuocTinh> BaoGia_Loai_ThuocTinh_GetAllBy_LoaiID(long loaiID, int _AppID)
        {
            using (var context = new BlueDataWebEntities())
            {
                List<BaoGia_Loai_ThuocTinh> data = context.BaoGia_Loai_ThuocTinh.Where(x => x.LoaiBaoGiaID == loaiID && x.AppID == _AppID).OrderBy(x => x.ID).ToList();

                return data;
            }
        }

        public bool BaoGia_Loai_ThuocTinh_UpdateNhieu(List<BaoGia_Loai_ThuocTinh> data, long _LoaiBaoGiaID, int _AppID)
        {
            bool result = false;
            try
            {
                using (var context = new BlueDataWebEntities())
                {
                    foreach (var item in data)
                    {
                        if (item.ID == 0)
                        {
                            //Them moi
                            context.BaoGia_Loai_ThuocTinh.Add(item);
                            item.AllowDelete = true;
                            item.IsDelete = false;
                            item.LoaiBaoGiaID = _LoaiBaoGiaID;
                        }
                        else
                        {
                            // Cap Nhat

                            //Get item
                            var entite = context.BaoGia_Loai_ThuocTinh.Where(x => x.ID == item.ID && x.LoaiBaoGiaID == item.LoaiBaoGiaID).FirstOrDefault();
                            if (entite != null)
                            {
                                entite.Ten = item.Ten;
                                entite.LoaiBaoGiaID = item.LoaiBaoGiaID;
                                entite.GiaTri = item.GiaTri;
                                entite.AllowDelete = item.AllowDelete;
                                entite.IsDelete = item.IsDelete;
                                entite.OrderBy = item.OrderBy;
                            }
                        }
                    }

                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
            }

            return result;
        }

        #endregion Thuộc tính Loại báo giá

        #region Quản lý báo giá

        public long BaoGia_Create(BaoGia model)
        {
            long idReturn = 0;
            try
            {
                using (var context = new BlueDataWebEntities())
                {
                    model.IsDelete = false;
                    context.BaoGias.Add(model);
                    context.SaveChanges();
                    idReturn = model.BaoGiaID;

                    if (idReturn > 0)
                    {
                        var lstTT_BaoGia = context.BaoGia_Loai_ThuocTinh.Where(x => x.LoaiBaoGiaID == model.LoaiBaoGiaID && x.AppID == model.AppID).ToList();
                        foreach (var item in lstTT_BaoGia)
                        {
                            BaoGia_GiaTri CCTT = new BaoGia_GiaTri();
                            CCTT.LoaiBaoGiaID = model.LoaiBaoGiaID;
                            CCTT.AppID = model.AppID;
                            CCTT.BaoGiaID = idReturn;
                            CCTT.TenThuocTinh = item.Ten;
                            CCTT.GiaTriThuocTinh = item.GiaTri;
                            CCTT.IsDefault = true;
                            CCTT.OrderBy = item.OrderBy;
                            context.BaoGia_GiaTri.Add(CCTT);
                        }
                        context.SaveChanges();
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                var erroDb = string.Empty;
                foreach (var eve in ex.EntityValidationErrors)
                {
                    erroDb += "-" + eve.Entry.Entity.GetType().Name + ": " + eve.Entry.State;

                    foreach (var ve in eve.ValidationErrors)
                    {
                        erroDb += "@" + ve.PropertyName + "@" + ve.ErrorMessage;
                    }
                }
                //log.Error(erroDb);
            }

            return idReturn;
        }

        public long BaoGia_GiaTri_Update(List<BaoGia_GiaTri> model, int _BaoGiaID, int _LoaiBaoGiaID)
        {
            long idReturn = 0;

            try
            {
                using (var context = new BlueDataWebEntities())
                {
                    foreach (var item in model)
                    {
                        if (item.Id > 0)
                        {
                            BaoGia_GiaTri CCTT = context.BaoGia_GiaTri.Where(x => x.AppID == item.AppID && x.Id == item.Id).FirstOrDefault();
                            CCTT.LoaiBaoGiaID = item.LoaiBaoGiaID;
                            CCTT.BaoGiaID = item.BaoGiaID;
                            CCTT.TenThuocTinh = item.TenThuocTinh;
                            CCTT.GiaTriThuocTinh = item.GiaTriThuocTinh;
                            CCTT.IsDefault = true;
                            context.SaveChanges();
                        }
                        else
                        {
                            item.LoaiBaoGiaID = _LoaiBaoGiaID;
                            item.BaoGiaID = _BaoGiaID; 
                            context.BaoGia_GiaTri.Add(item); 
                            context.SaveChanges();
                        }
                    }
                    context.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                var erroDb = string.Empty;
                foreach (var eve in ex.EntityValidationErrors)
                {
                    erroDb += "-" + eve.Entry.Entity.GetType().Name + ": " + eve.Entry.State;

                    foreach (var ve in eve.ValidationErrors)
                    {
                        erroDb += "@" + ve.PropertyName + "@" + ve.ErrorMessage;
                    }
                }
                //log.Error(erroDb);
            }

            return idReturn;
        }

        public long BaoGia_Update(BaoGia model)
        {
            long result = 0;
            try
            {
                using (var context = new BlueDataWebEntities())
                {

                    context.Entry(model).State = EntityState.Modified;
                    context.SaveChanges();

                    //BaoGia data = context.BaoGias.Where(x => x.BaoGiaID == model.BaoGiaID).FirstOrDefault();
                    //data.LoaiBaoGiaID = model.LoaiBaoGiaID;

                    //data.Ten = model.Ten;
                    //data.AppID = model.AppID;
                    //data.LoaiBaoGiaID = model.LoaiBaoGiaID;
                    //data.Ten = model.Ten;
                    //data.MoTa = model.MoTa;
                    //data.MoTaNgan = model.MoTaNgan;
                    //data.MetaTitle = model.MetaTitle;
                    //data.MetaKeyword = model.MetaKeyword;
                    //data.MetaDescription = model.MetaDescription;
                    //data.IsDelete = model.IsDelete;
                    //data.HinhAnh = model.HinhAnh;
                    //data.DocumentFile = model.DocumentFile;
                    //data.Video = model.Video;
                    //data.OrderBy = model.OrderBy;

                    //context.SaveChanges();
                    result = model.BaoGiaID;
                }
            }
            catch (Exception ex)
            {
                //log.Error(ex.Message);
            }
            return result;
        }

        public BaoGia BaoGia_Get_ByID(long _BaoGiaID)
        {
            using (var context = new BlueDataWebEntities())
            {
                BaoGia data = context.BaoGias.Where(x => x.BaoGiaID == _BaoGiaID).FirstOrDefault();

                return data;
            }
        }

        public List<BaoGia> BaoGia_GetAll(long _AppID, long _loaiBaogia, int take = 999999)
        {
            using (var context = new BlueDataWebEntities())
            {
                List<BaoGia> data = new List<BaoGia>();
                if (take == 999999)
                {
                    data = context.BaoGias.Where(x => x.IsDelete == false && x.AppID == _AppID && x.LoaiBaoGiaID == _loaiBaogia)
                   .OrderBy(x => x.OrderBy).ThenByDescending(x => x.BaoGiaID).ToList();
                }
                else
                {
                    data = context.BaoGias.Where(x => x.IsDelete == false && x.AppID == _AppID && x.LoaiBaoGiaID == _loaiBaogia)
                 .OrderBy(x => x.OrderBy).ThenByDescending(x => x.BaoGiaID).Take(take).ToList();
                }

                return data;
            }
        }

        public List<BaoGia_GiaTri> BaoGia_GiaTri_GetAll(long _AppID, long _BaoGiaID, long _LoaiBaoGia)
        {
            using (var context = new BlueDataWebEntities())
            {
                List<BaoGia_GiaTri> data = context.BaoGia_GiaTri.Where(x => x.BaoGiaID == _BaoGiaID && x.AppID == _AppID && x.LoaiBaoGiaID == _LoaiBaoGia)
                    .OrderBy(x => x.OrderBy).ThenByDescending(x => x.BaoGiaID).ToList();

                return data;
            }
        }
       

        #endregion Quản lý báo giá
    }
}