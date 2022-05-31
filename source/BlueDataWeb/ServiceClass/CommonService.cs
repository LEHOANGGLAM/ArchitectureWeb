using BlueDataWeb.Common;
using BlueDataWeb.Models.CustomsModel;
using BlueDataWeb.Models.Entites;
using BlueDataWeb.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace BlueDataWeb.ServiceClass.Common
{
    public class CommonService
    {
        private int AppID { get; set; }

        public CommonService()
        {
            AppID = int.Parse(System.Configuration.ConfigurationManager.AppSettings["AppID"].ToString());
        }

        public List<NewCategory> GetNewNewCategory()
        {
            List<NewCategory> result = new List<NewCategory>();
            try
            {
                var cache = MemoryCache.Default;
                cache = MemoryCache.Default;

                if (cache.Get(AppID.ToString() + "MemoryCache_GetNewNewCategory") == null)
                {
                    var cachePolicty = new CacheItemPolicy();
                    cachePolicty.AbsoluteExpiration = DateTime.Now.AddHours(12);
                    cache = MemoryCache.Default;

                    using (var db = new BlueDataWebEntities())
                    {
                        result = db.NewCategories.AsNoTracking().Where(x => x.AppID == this.AppID).ToList();

                        cache.Add(AppID.ToString() + "MemoryCache_GetNewNewCategory", result, cachePolicty);
                    }
                }
                else
                {
                    result = (List<NewCategory>)cache.Get(AppID.ToString() + "MemoryCache_GetNewNewCategory");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public List<ArchitectureCategory> GetKienTrucCategory()
        {
            List<ArchitectureCategory> result = new List<ArchitectureCategory>();
            try
            {
                var cache = MemoryCache.Default;
                cache = MemoryCache.Default;

                if (cache.Get(AppID.ToString() + "MemoryCache_GetKienTrucCategory") == null)
                {
                    var cachePolicty = new CacheItemPolicy();
                    cachePolicty.AbsoluteExpiration = DateTime.Now.AddHours(12);
                    cache = MemoryCache.Default;

                    using (var db = new BlueDataWebEntities())
                    {
                        result = db.ArchitectureCategories.AsNoTracking().Where(x => x.AppID == this.AppID && x.IsDeleted ==false).ToList();

                        cache.Add(AppID.ToString() + "MemoryCache_GetKienTrucCategory", result, cachePolicty);
                    }
                }
                else
                {
                    result = (List<ArchitectureCategory>)cache.Get(AppID.ToString() + "MemoryCache_GetKienTrucCategory");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }


        public List<NewViewModel> GetTinTucFooter()
        {
            List<NewViewModel> result = new List<NewViewModel>();
            try
            {
                var cache = MemoryCache.Default;
                cache = MemoryCache.Default;

                if (cache.Get(AppID.ToString() + "MemoryCache_GetCaseStudy") == null)
                {
                    var cachePolicty = new CacheItemPolicy();
                    cachePolicty.AbsoluteExpiration = DateTime.Now.AddHours(12);
                    cache = MemoryCache.Default;

                    using (var db = new BlueDataWebEntities())
                    {

                        string _keyName = NewsType.E_TinTuc.ToString();
                        result = db.News.AsNoTracking().OrderByDescending(x => x.NewsID).Where(x => x.KeyName == _keyName && (x.isCaseStudy == false) && x.AppID == this.AppID && x.IsDelete == false)
                   .Select(x => new NewViewModel
                   {
                       Title = x.Title,
                       NewsID = x.NewsID,
                       ShortDescription = x.ShortDescription,
                       ImagePath = x.ImagePath,
                       NewCategoryName = x.NewCategory.Name,
                       CreatedDate = x.CreatedDate
                   }).Take(2).ToList();

                        cache.Add(AppID.ToString() + "MemoryCache_GetCaseStudy", result, cachePolicty);
                    }
                }
                else
                {
                    result = (List<NewViewModel>)cache.Get(AppID.ToString() + "MemoryCache_GetCaseStudy");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public List<News> GetGiaiPhap()
        {
            List<News> result = new List<News>();
            try
            {
                var cache = MemoryCache.Default;
                cache = MemoryCache.Default;

                if (cache.Get(AppID.ToString() + "MemoryCache_GetGiaiPhap") == null)
                {
                    var cachePolicty = new CacheItemPolicy();
                    cachePolicty.AbsoluteExpiration = DateTime.Now.AddHours(12);
                    cache = MemoryCache.Default;

                    using (var db = new BlueDataWebEntities())
                    {
                        NewCategory giapPhap_cate = db.NewCategories.Where(m => m.KeyName == "E_GiaiPhap" && m.AppID == this.AppID).FirstOrDefault();


                        if (giapPhap_cate != null)
                        {
                            result = db.News.Where(x => x.AppID == this.AppID && (x.IsDelete == false || x.IsDelete == null) && x.NewCategoriesID == giapPhap_cate.NewCategoriesID).ToList();

                        }
                        cache.Add(AppID.ToString() + "MemoryCache_GetGiaiPhap", result, cachePolicty);


                    }
                }
                else
                {
                    result = (List<News>)cache.Get(AppID.ToString() + "MemoryCache_GetGiaiPhap");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }


        public List<News> GetUngDung()
        {
            List<News> result = new List<News>();
            try
            {
                var cache = MemoryCache.Default;
                cache = MemoryCache.Default;

                if (cache.Get(AppID.ToString() + "MemoryCache_GetUngDung") == null)
                {
                    var cachePolicty = new CacheItemPolicy();
                    cachePolicty.AbsoluteExpiration = DateTime.Now.AddHours(12);
                    cache = MemoryCache.Default;

                    using (var db = new BlueDataWebEntities())
                    {
                        NewCategory giapPhap_cate = db.NewCategories.Where(m => m.KeyName == "E_UngDung" && m.AppID == this.AppID).FirstOrDefault();


                        if (giapPhap_cate != null)
                        {
                            result = db.News.Where(x => x.AppID == this.AppID && (x.IsDelete == false || x.IsDelete == null) && x.NewCategoriesID == giapPhap_cate.NewCategoriesID).ToList();

                        }
                        cache.Add(AppID.ToString() + "MemoryCache_GetUngDung", result, cachePolicty);


                    }
                }
                else
                {
                    result = (List<News>)cache.Get(AppID.ToString() + "MemoryCache_GetUngDung");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }


        public List<SettingDB> GetSetting()
        {
            List<SettingDB> result = new List<SettingDB>();
            try
            {
                var cache = MemoryCache.Default;
                cache = MemoryCache.Default;

                if (cache.Get(AppID.ToString() + "MemoryCache_GetSetting") == null)
                {
                    var cachePolicty = new CacheItemPolicy();
                    cachePolicty.AbsoluteExpiration = DateTime.Now.AddHours(12);
                    cache = MemoryCache.Default;

                    using (var db = new BlueDataWebEntities())
                    {
                        result=  db.SettingDBs.AsNoTracking().ToList();


                      
                        cache.Add(AppID.ToString() + "MemoryCache_GetSetting", result, cachePolicty);


                    }
                }
                else
                {
                    result = (List<SettingDB>)cache.Get(AppID.ToString() + "MemoryCache_GetSetting");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }



        public List<News> GetDichVu(int take)
        {
            List<News> result = new List<News>();
            try
            {
                var cache = MemoryCache.Default;
                cache = MemoryCache.Default;

                if (cache.Get(AppID.ToString() + "MemoryCache_GetDichVu") == null)
                {
                    var cachePolicty = new CacheItemPolicy();
                    cachePolicty.AbsoluteExpiration = DateTime.Now.AddHours(12);
                    cache = MemoryCache.Default;

                    using (var db = new BlueDataWebEntities())
                    {
                        NewCategory dichVu_cate = db.NewCategories.Where(m => m.KeyName == "E_DichVu" && m.AppID == this.AppID).FirstOrDefault();
                        if (dichVu_cate != null)
                        {
                            result = db.News.AsNoTracking().Where(x => x.AppID == this.AppID && x.NewCategoriesID == dichVu_cate.NewCategoriesID).Take(take).ToList();

                        }

                            cache.Add(AppID.ToString() + "MemoryCache_GetDichVu", result, cachePolicty);

                    }
                }
                else
                {
                    result = (List<News>)cache.Get(AppID.ToString() + "MemoryCache_GetDichVu");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }


        public List<BaoGia> GetBaoGa()
        {
            List<BaoGia> result = new List<BaoGia>();
            try
            {
                var cache = MemoryCache.Default;
                cache = MemoryCache.Default;

                if (cache.Get(AppID.ToString() + "MemoryCache_GetBaoGa") == null)
                {
                    var cachePolicty = new CacheItemPolicy();
                    cachePolicty.AbsoluteExpiration = DateTime.Now.AddHours(12);
                    cache = MemoryCache.Default;

                    using (var db = new BlueDataWebEntities())
                    {
                        result = db.BaoGias.Where(x => x.AppID == this.AppID && x.LoaiBaoGiaID == 1).ToList();

                        cache.Add(AppID.ToString() + "MemoryCache_GetBaoGa", result, cachePolicty);
                    }
                }
                else
                {
                    result = (List<BaoGia>)cache.Get(AppID.ToString() + "MemoryCache_GetBaoGa");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }


        public List<News> GetHuongDan()
        {
            List<News> result = new List<News>();
            try
            {
                var cache = MemoryCache.Default;
                cache = MemoryCache.Default;

                if (cache.Get(AppID.ToString() + "MemoryCache_GetHuongDan") == null)
                {
                    var cachePolicty = new CacheItemPolicy();
                    cachePolicty.AbsoluteExpiration = DateTime.Now.AddHours(12);
                    cache = MemoryCache.Default;

                    using (var db = new BlueDataWebEntities())
                    {

                        NewCategory cate = db.NewCategories.Where(m => m.KeyName == "E_HuongDan" && m.AppID == this.AppID).FirstOrDefault();
                        if(cate != null)
                        {
                            result = db.News.AsNoTracking().Where(x => x.AppID == this.AppID && x.NewCategoriesID == cate.NewCategoriesID).ToList();

                        }
                   

                        cache.Add(AppID.ToString() + "MemoryCache_GetHuongDan", result, cachePolicty);
                    }
                }
                else
                {
                    result = (List<News>)cache.Get(AppID.ToString() + "MemoryCache_GetHuongDan");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public bool clearALlcaching()
        {
            bool result = false;

            try
            {

                
                CacheHelper cache_manager = new CacheHelper();
                cache_manager.Remove(AppID + "MemoryCache_GetNewNewCategory");
                cache_manager.Remove(AppID + "MemoryCache_GetTinTucFooter");
                cache_manager.Remove(AppID + "MemoryCache_GetGiaiPhap");
                cache_manager.Remove(AppID + "MemoryCache_GetDichVu");
                cache_manager.Remove(AppID + "MemoryCache_GetBaoGa");
                cache_manager.Remove(AppID + "MemoryCache_GetHuongDan");
                cache_manager.Remove(AppID + "MemoryCache_GetUngDung");
                cache_manager.Remove(AppID + "MemoryCache_GetSetting");

                
                result = true;
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }
    }
}