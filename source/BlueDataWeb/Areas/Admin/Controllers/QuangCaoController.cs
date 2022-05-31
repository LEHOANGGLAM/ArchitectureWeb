using BlueDataWeb.Areas.Admin.Models.Service;
using BlueDataWeb.Models;
using BlueDataWeb.Models.CustomsModel;
using BlueDataWeb.Models.Entites;
using BlueDataWeb.Utility;
using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueDataWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, AdminSlider")]
    public class QuangCaoController : Controller
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();
        private ImageService imgservice = new ImageService();

        public ActionResult Index()
        {
            var listQuangCao = db.QuangCaos;
            return View(listQuangCao.ToList());
        }

        //

        public ActionResult Create()
        {
            QuangCaoModels qc = new QuangCaoModels();

            return View(qc);
        }

        //
        // POST: /Admin/Image/Create

        //[HttpPost]
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(QuangCaoModels quangcao)
        {
            if (ModelState.IsValid)
            {
               

                #region xử lý upload image

                if (Request.Files["img"] != null)
                {
                    string fullname = Request.Files["img"].FileName;
                    if (fullname != "")
                    {
                        quangcao.Image = UploadImageHelper.Upload(Request.Files["img"], Server.MapPath("~/Images/"), "News");
                    }
                }

                #endregion xử lý upload image



                QuangCao qc = new QuangCao();
                qc.QuangCaoID = quangcao.QuangCaoID;
                qc.Name = quangcao.Name;
                qc.Description = quangcao.Description;
                qc.Link = quangcao.Link;
                qc.IsShow = quangcao.IsShow;
                qc.Width = quangcao.Width;
                qc.Height = quangcao.Height;
                qc.Image = quangcao.Image;
                qc.PositionID = quangcao.PositionID;

                db.QuangCaos.Add(qc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quangcao);
        }

        //
        // GET: /Admin/Image/Edit/5

        public ActionResult Edit(int id = 0)
        {
            QuangCao qc = db.QuangCaos.Find(id);
            QuangCaoModels qcView = new QuangCaoModels();
            qcView.QuangCaoID = qc.QuangCaoID;
            qcView.Name = qc.Name;
            qcView.Description = qc.Description;
            qcView.Link = qc.Link;
            qcView.IsShow = qc.IsShow;
            qcView.Width = qc.Width;
            qcView.Height = qc.Height;
            qcView.Image = qc.Image;
            qcView.PositionID = qc.PositionID;

            if (qc == null)
            {
                return HttpNotFound();
            }
            return View(qcView);
        }

        //
        // POST: /Admin/Image/Edit/5
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(QuangCaoModels qcView)
        {
            if (ModelState.IsValid)
            {

                #region xử lý upload image

                if (Request.Files["img"] != null)
                {
                    string fullname = Request.Files["img"].FileName;
                    if (fullname != "")
                    {
                        qcView.Image = UploadImageHelper.Upload(Request.Files["img"], Server.MapPath("~/Images/"), "News");
                    }
                }

                #endregion xử lý upload image


                QuangCao qc = new QuangCao();
                qc.QuangCaoID = qcView.QuangCaoID;
                qc.Name = qcView.Name;
                qc.Description = qcView.Description;
                qc.Link = qcView.Link;
                qc.IsShow = qcView.IsShow;
                qc.Width = qcView.Width;
                qc.Height = qcView.Height;
                qc.Image = qcView.Image;
                qc.PositionID = qcView.PositionID;

                db.Entry(qc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(qcView);
        }

        //
        // GET: /Admin/Image/Delete/5

        public ActionResult Delete(int id = 0)
        {
            QuangCao qc = db.QuangCaos.Find(id);
            if (qc == null)
            {
                return HttpNotFound();
            }
            return View(qc);
        }

        //
        // POST: /Admin/Image/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            QuangCao qc = db.QuangCaos.Find(id);
            db.QuangCaos.Remove(qc);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}