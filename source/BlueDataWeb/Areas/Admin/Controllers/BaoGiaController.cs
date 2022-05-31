using BlueDataWeb.Helpers;
using BlueDataWeb.Models.CustomsModel;
using BlueDataWeb.Models.Entites;
using BlueDataWeb.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using WebMVC.Entities;

namespace BlueDataWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, AdminNews")]
    public class BaoGiaController : BaseController
    {
        private BaoGiaService srv = new BaoGiaService();

        //
        // GET: /Admin/News/

        public ActionResult Index(string idCate)
        {
            var allData = new List<BaoGia>();
            allData = srv.BaoGia_GetAll(this.AppID, 1);

            return View(allData);
        }

        //

        //
        // GET: /Admin/News/Create

        public ActionResult Create()
        {
            var data = new BaoGiaModel();
            data.AppID = this.AppID;
            data.LoaiBaoGiaID = 1;
            data.IsDelete = false;

            return View(data);
        }

        //
        // POST: /Admin/News/Create

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(BaoGia news)
        {
            if (ModelState.IsValid)
            {

                #region xử lý upload image

                if (Request.Files["img"] != null)
                {
                    string fullname = Request.Files["img"].FileName;
                    if (fullname != "")
                    {
                        news.HinhAnh = UploadImageHelper.Upload(Request.Files["img"], Server.MapPath("~/Images/"), "News");
                    }
                }

                #endregion xử lý upload image

 

                if (news.OrderBy == null)
                {
                    news.OrderBy = 0;
                }

                srv.BaoGia_Create(news);

                return RedirectToAction("Index");
            }

            return View(news);
        }

        //
        // GET: /Admin/News/Edit/5

        public ActionResult Edit(int id = 0)
        {
            BaoGia news = srv.BaoGia_Get_ByID(id);
            BaoGiaModel model = new BaoGiaModel();

            model = model.UpdateModelToView(news);

            if (news == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        //
        // POST: /Admin/News/Edit/5

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(BaoGia model)
        {
            if (ModelState.IsValid)
            {
                

                #region xử lý upload image

                if (Request.Files["img"] != null)
                {
                    string fullname = Request.Files["img"].FileName;
                    if (fullname != "")
                    {
                        model.HinhAnh = UploadImageHelper.Upload(Request.Files["img"], Server.MapPath("~/Images/"), "News");
                    }
                }

                #endregion xử lý upload image



                if (Request.Files["filepdf"] != null)
                {
                    HttpPostedFileBase file0 = Request.Files["filepdf"];
                    string fullname = Request.Files["filepdf"].FileName;

                    if (fullname != "")
                    {
                        
                        string path = Server.MapPath("~/Images/News/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        if (fullname != "")
                        {
                            var filesavename = DateTime.Now.Ticks.ToString() + file0.FileName;
                            file0.SaveAs(path + Path.GetFileName(filesavename));
                            model.FileBaoGia = filesavename;
                        }


                       
                    }
                }




                if (model.OrderBy == null)
                {
                    model.OrderBy = 0;
                }
                srv.BaoGia_Update(model);

                return RedirectToAction("Index");
            }
            return View(model);
        }

        //
        // GET: /Admin/News/Delete/5

        public ActionResult Delete(int id = 0)
        {
            BaoGia news = srv.BaoGia_Get_ByID(id);

            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        //
        // POST: /Admin/News/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            BaoGia news = srv.BaoGia_Get_ByID(id);

            news.IsDelete = true;
            srv.BaoGia_Update(news);

            return RedirectToAction("Index");
        }

        public ActionResult DetailBangGia(int baogiaID)
        {
            BaoGia news = srv.BaoGia_Get_ByID(baogiaID);
            ViewBag.BaoGia = news;
            ViewBag.baogiaID = baogiaID;
            List<BaoGia_GiaTri> baoGiaChiTiet = srv.BaoGia_GiaTri_GetAll(this.AppID, baogiaID, 1);
            return View(baoGiaChiTiet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DetailBangGia(string baogiaID, string JsonThuocTinh)
        {
            List<BaoGia_GiaTri> lstModel = new List<BaoGia_GiaTri>();

            lstModel = JsonConvert.DeserializeObject<List<BaoGia_GiaTri>>(JsonThuocTinh);

            srv.BaoGia_GiaTri_Update(lstModel, int.Parse(baogiaID), 1);

            return RedirectToAction("Index");
        }


         

        //public ActionResult DownloadFile(string fileName)
        //{
        //    var dir = new System.IO.DirectoryInfo(Server.MapPath("/Images/News/"));
        //    System.IO.FileInfo[] fileNames = dir.GetFiles("*.*"); List<string> items = new List<string>();
        //    foreach (var file in fileNames)
        //    {
        //        items.Add(file.Name);
        //    }
        //    return View(items);
        //}
        public FileResult DownloadFile(string fileName)
        {
            var FileVirtualPath = "/Images/News/" + fileName;
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
        }


    }
}