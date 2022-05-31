using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueDataWeb.Models.Entites;
using BlueDataWeb.Utility;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using BlueDataWeb.Models.CustomsModel;

namespace BlueDataWeb.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin, AdminCustomer")]
    public class CustomerController : Controller
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();

        //
        // GET: /Admin/Customer/
        //mã hóa md5 pass
        public static byte[] encryptData(string data)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashedBytes;
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(data));
            return hashedBytes;
        }
        public static string md5(string data)
        {
            return BitConverter.ToString(encryptData(data)).Replace("-", "").ToLower();

            //
            // GET: /User/Edit/5
        }
        public ActionResult Index(string showall=null)
        {
            if (showall == null)
            {
                int pageNo = 0;
                foreach (var key in Request.Form.AllKeys)
                {
                    if (key.StartsWith("PageNo:"))
                    {
                        pageNo = int.Parse(key.Substring(7));
                        break;
                    }
                }
                PagerInfo pi = PagerInfo.Get("Customer", 20);
                pi.RowCount = db.Users.OrderByDescending(p => p.Name).Count();
                pi.Navigate(pageNo);
                int StartRow = pi.PageNo * pi.PageSize;
                var custmers = db.Users.OrderByDescending(a => a.Name).Skip(StartRow).Take(pi.PageSize);
                return View(custmers);
            }
            else
            {
                var customer = db.Users.OrderByDescending(o => o.Name);
                return View(customer);
            }
        }
        [HttpPost, ActionName("Index")]
        public ActionResult IndexAjax(string showall=null)
        {
            if (showall == null)
            {
            int pageNo = 0;
            foreach (var key in Request.Form.AllKeys)
            {
                if (key.StartsWith("PageNo:"))
                {
                    pageNo = int.Parse(key.Substring(7));
                    break;
                }
            }
            PagerInfo pi = PagerInfo.Get("Customer", 20);
            pi.RowCount = db.Users.OrderByDescending(p => p.Name).Count();
            pi.Navigate(pageNo);
            int StartRow = pi.PageNo * pi.PageSize;
            var custmers = db.Users.OrderByDescending(a => a.Name).Skip(StartRow).Take(pi.PageSize);
            return PartialView("IndexAjax",custmers);
            }
            else
            {
                var customer = db.Users.OrderByDescending(o => o.Name);
                return View(customer);
            }
        }

        //
        // GET: /Admin/Customer/Details/5

        public ActionResult Details(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // GET: /Admin/Customer/Create

        public ActionResult Create()
        {
            ViewBag.City_Province = new SelectList(db.Provices, "Id", "Tentinhthanh");
            ViewBag.DistrictId = new SelectList(db.Districts, "Id", "Tenquanhuyen");
            ViewBag.WardId = new SelectList(db.Wards, "WardId", "Tenphuongxa");
            return View();
        }

        //
        // POST: /Admin/Customer/Create

        [HttpPost]
        public ActionResult Create(User user, string Birthday)
        {
            user.Birthday = ConvertMulti.ConvertStringToDateTime_Split(Birthday);
            ModelState.Remove("Birthday");
            if (ModelState.IsValid)
            {
                String encryppass = md5(user.Password);
                user.Status = false;
                user.IsActive = true;
                user.CodeActive = "?" + md5(user.Email);
                user.Password = encryppass;
                db.Users.Add(user);
                db.SaveChanges();
               
                return RedirectToAction("Index");
            }
            ViewBag.Provices = new SelectList(db.Provices.ToList(), "ID", "TenTinhThanh", user.City_Province);
            ViewBag.Districts = new SelectList(db.Districts.Where(m => m.MaTinhThanh == user.City_Province).ToList(), "ID", "TenQuanHuyen", user.District);
            ViewBag.Wards = new SelectList(db.Wards.Where(m => m.MaQuanHuyen == user.District).ToList(), "ID", "TenPhuongXa", user.Ward);
            return View(user);
        }

        //
        // GET: /Admin/Customer/Edit/5

        public ActionResult Edit(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Provices = new SelectList(db.Provices.ToList(), "ID", "TenTinhThanh", user.City_Province);
            ViewBag.Districts = new SelectList(db.Districts.Where(m => m.MaTinhThanh == user.City_Province).ToList(), "ID", "TenQuanHuyen", user.District);
            ViewBag.Wards = new SelectList(db.Wards.Where(m => m.MaQuanHuyen == user.District).ToList(), "ID", "TenPhuongXa", user.Ward);
            return View(user);
        }

        //
        // POST: /Admin/Customer/Edit/5

        [HttpPost]
        public ActionResult Edit(User user, string Birthday)
        {
            user.Birthday = ConvertMulti.ConvertStringToDateTime_Split(Birthday);
            ModelState.Remove("Birthday");
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();               
                return RedirectToAction("Index");
            }
            ViewBag.Provices = new SelectList(db.Provices.ToList(), "ID", "TenTinhThanh", user.City_Province);
            ViewBag.Districts = new SelectList(db.Districts.Where(m => m.MaTinhThanh == user.City_Province).ToList(), "ID", "TenQuanHuyen", user.District);
            ViewBag.Wards = new SelectList(db.Wards.Where(m => m.MaQuanHuyen == user.District).ToList(), "ID", "TenPhuongXa", user.Ward);
            return View(user);
        }

        //
        // GET: /Admin/Customer/Delete/5

        public ActionResult Delete(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Admin/Customer/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            var comment = db.Comments.Where(c => c.UserID == id);
            foreach (var c in comment)
            {
                db.Comments.Remove(c);
            }
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        [HttpPost]
        public ActionResult ExportToExcel()
        {
            var customer_tb = new System.Data.DataTable("test");
            //định dạng cột
            customer_tb.Columns.Add("STT", typeof(int));
            customer_tb.Columns.Add("Tên khách hàng", typeof(String));
            customer_tb.Columns.Add("Địa chỉ", typeof(string));
            customer_tb.Columns.Add("Email", typeof(string));
            customer_tb.Columns.Add("Điện thoại nhà", typeof(string));
            //products.Columns.Add("Ghi chú", typeof(string));
            customer_tb.Columns.Add("Di động", typeof(string));
            customer_tb.Columns.Add("Ngày sinh", typeof(string));
            //products.Columns.Add("Phí giao hàng", typeof(string));
           
            int flag = 0;
            var customer = db.Users;
            foreach (var c in customer)
            {
                 var chk = Request.Form["chk" + c.Id];
                 if (chk == "on")
                 {

                     flag++;
                     String add = c.District + c.Ward + c.Street + c.Floor;
                     customer_tb.Rows.Add(flag, c.Name, add, c.Email, c.Home_Number, c.Phone, c.Birthday);
                 }
            }
            
                    
            String file = "DanhSachKhachhang.xls";
            var grid = new GridView();
            grid.DataSource = customer_tb;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=" + file + "");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "utf-8";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View("Index");
        }
    }
}