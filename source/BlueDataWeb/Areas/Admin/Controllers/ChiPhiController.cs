using BlueDataWeb.Models.CustomsModel;
using BlueDataWeb.Models.Entites;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace BlueDataWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, AdminNews")]
    public class ChiPhiController : BaseController
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();

        public ActionResult Index()
        {
            var data = db.Prices.Where(x => x.AppID == this.AppID).ToList();
            return View(data);
        }

        public ActionResult Create()
        {
            PriceModel data = new PriceModel();
            data.AppID = this.AppID;
            return View(data);
        }

        //
        // POST: /Admin/News/Create

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(PriceModel data)
        {
            if (ModelState.IsValid)
            {
                Price model = data.Map_To_Price();

                db.Prices.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                string validationErrors = string.Join("<br/>",
                 ModelState.Values.Where(E => E.Errors.Count > 0)
                 .SelectMany(E => E.Errors)
                 .Select(E => E.ErrorMessage)
                 .ToArray());

                ViewBag.SaveResult = validationErrors;
            }
            return View(data);
        }

        //
        // GET: /Admin/News/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Price dataget = db.Prices.Where(x => x.AppID == this.AppID && x.Id == id).FirstOrDefault();
            PriceModel data = new PriceModel();

            if (dataget == null)
            {
                return HttpNotFound();
            }

            data.Map_From_Price(dataget);
            return View(data);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(PriceModel model)
        {
            if (ModelState.IsValid)
            {
                Price dataget = db.Prices.Where(x => x.AppID == this.AppID && x.Id == model.Id).FirstOrDefault();
                dataget.AppID = model.AppID;
                dataget.Name = model.Name;
                dataget.Description = model.Description;
                dataget.PriceChucNang = model.PriceChucNang;
                dataget.Active = model.Active;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //
        // GET: /Admin/News/Delete/5

        public ActionResult Delete(int id = 0)
        {
            var news = db.Prices.Find(id);
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
            Price data = db.Prices.Find(id);

            db.Prices.Remove(data);
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