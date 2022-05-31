using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueDataWeb.Models.Entites;

namespace BlueDataWeb.Areas.Admin.Controllers
{
    public class SettingABCController : BaseController
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();
        public ActionResult ClearCache()
        {
            BlueDataWeb.ServiceClass.Common.CommonService srv = new BlueDataWeb.ServiceClass.Common.CommonService();

            bool result = srv.clearALlcaching();
            return View(result);
        }
        //
        // GET: /Admin/SettingDB/

        public ActionResult Index()
        {
            return View(db.SettingDBs.Where(x=>x.AppID == this.AppID).OrderBy(x=>x.Ma).ToList());

        }

        public ActionResult Uplogo()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UplogoPost(string loai)
        {
            try
            {
                if (Request.Files["img"] != null)
                {
                    HttpPostedFileBase file0 = Request.Files["img"];
                    string fullname = "";
                    switch (loai)
                    {
                        case "logo":
                            {
                                fullname = "logo.png";
                                break;
                            }
                        case "banner":
                            {
                                fullname = "banner.jpg";

                                break;
                            }
                        case "banner2":
                            {
                                fullname = "banner2.jpg";

                                break;
                            }
                        default:
                            break;
                    }


                    string path = Server.MapPath("~/Images/Logo/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    file0.SaveAs(path + fullname);
                }

                ViewBag.Result = "Cập nhật thành công";
            }
            catch (Exception ex)
            {
                throw;
            }

            return View("Uplogo");
        }

        //
        // GET: /Admin/SettingDB/Details/5

        public ActionResult Details(long id = 0)
        {
            SettingDB settingdb = db.SettingDBs.Find(id);
            if (settingdb == null)
            {
                return HttpNotFound();
            }
            return View(settingdb);
        }

        //
        // GET: /Admin/SettingDB/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/SettingDB/Create

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(SettingDB settingdb)
        {
            if (ModelState.IsValid)
            {
                settingdb.AppID = this.AppID;
                db.SettingDBs.Add(settingdb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(settingdb);
        }

        //
        // GET: /Admin/SettingDB/Edit/5

        public ActionResult Edit(long id = 0)
        {
            SettingDB settingdb = db.SettingDBs.Find(id);
            if (settingdb == null)
            {
                return HttpNotFound();
            }
            return View(settingdb);
        }

        //
        // POST: /Admin/SettingDB/Edit/5

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(SettingDB settingdb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(settingdb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(settingdb);
        }

        //
        // GET: /Admin/SettingDB/Delete/5

        public ActionResult Delete(long id = 0)
        {
            SettingDB settingdb = db.SettingDBs.Find(id);
            if (settingdb == null)
            {
                return HttpNotFound();
            }
            return View(settingdb);
        }

        //
        // POST: /Admin/SettingDB/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            SettingDB settingdb = db.SettingDBs.Find(id);
            db.SettingDBs.Remove(settingdb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        #region Update files

        public ActionResult UpFavianIcon()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpFavianIconPost()
        {
            //< link rel = "shortcut icon" href = "~/Themes/NMT/icon/favicon.png" >

            try
            {
                if (Request.Files["img"] != null)
                {
                    HttpPostedFileBase file0 = Request.Files["img"];
                    string fullname = "favicon.png";

                    string path = Server.MapPath("~/Images/Logo/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    file0.SaveAs(path + fullname);
                }

                ViewBag.Result = "Cập nhật thành công";
            }
            catch (Exception ex)
            {
                throw;
            }

            return View("UpFavianIcon");
        }



        public ActionResult UpRobotFile()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpRobotFilePost()
        {
            //< link rel = "shortcut icon" href = "~/Themes/NMT/icon/favicon.png" >

            try
            {
                if (Request.Files["img"] != null)
                {
                    HttpPostedFileBase file0 = Request.Files["img"];
                    string fullname = "robots.txt";

                    string path = Server.MapPath("~/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    file0.SaveAs(path + fullname);
                }

                ViewBag.Result = "Cập nhật thành công";
            }
            catch (Exception ex)
            {
                throw;
            }

            return View("UpRobotFile");
        }


        public ActionResult UpSiteMapFile()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpSiteMapFilePost()
        {
            //< link rel = "shortcut icon" href = "~/Themes/NMT/icon/favicon.png" >

            try
            {
                if (Request.Files["img"] != null)
                {
                    HttpPostedFileBase file0 = Request.Files["img"];
                    string fullname = "sitemap.xml";

                    string path = Server.MapPath("~/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    file0.SaveAs(path + fullname);
                }

                ViewBag.Result = "Cập nhật thành công";
            }
            catch (Exception ex)
            {
                throw;
            }

            return View("UpSiteMapFile");
        }



        #endregion Update files

    }
}
