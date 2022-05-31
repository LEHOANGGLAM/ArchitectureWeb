using BlueDataWeb.Models.CustomsModel;
using BlueDataWeb.Models.Entites;
using BlueDataWeb.Models.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.IO;

namespace BlueDataWeb.Controllers
{
    public class ContactController : BaseController
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();
        private EmailService emailserice = new EmailService();

        public ActionResult Index(string Name , string Email, string TenCongTy = "", int banggia= 0)
        {
            ContactModels contactmodel = new ContactModels();
            if(!string.IsNullOrWhiteSpace(Name))
            {
                contactmodel.Name = Name;
            }
            if (!string.IsNullOrWhiteSpace(Email))
            {
                contactmodel.Email = Email;
            }


            if (!string.IsNullOrWhiteSpace(TenCongTy))
            {
                contactmodel.CongTy = TenCongTy;
            }

            contactmodel.AppID = this.AppID;
            contactmodel.BangGiaID = banggia;

            var lstBaiVietGioiThieu = db.Abouts.AsNoTracking().Where(x => x.AppID == this.AppID).ToList();
            ViewBag.GioiThieu = lstBaiVietGioiThieu;
            return View(contactmodel);
        }

        [HttpPost]
        public ActionResult Index(ContactModels _contact, SubEmail sub)
        {
            if (ModelState.IsValid)
            {
                string DichVu = string.Empty;
                string MoTaDichVu = string.Empty;
                Contact contact = new Contact();
                contact.Name = _contact.Name;
                contact.Email = _contact.Email;
                contact.Phone = _contact.Phone;
                contact.contents = _contact.contents;
                contact.Address = _contact.Address;
                contact.TypeContact = "E_DichVu";
                contact.Status = "E_DangKyMoi";
                contact.CreateDate = DateTime.Now;
                contact.ChucVu = _contact.ChucVu;
                contact.LinhVuc = _contact.LinhVuc;
                contact.AppID = this.AppID;
                contact.BangGiaID = _contact.BangGiaID;

                if (contact.BangGiaID > 0)
                {
                    //Get chi tiết bảng giá
                    var bangGia=  db.BaoGias.Where(x => x.BaoGiaID == contact.BangGiaID).FirstOrDefault();
                    if (bangGia != null)
                    {
                        contact.BangGiaChiTiet = bangGia.Ten + " Tổng giá: " + bangGia.TongGia;
                    }
                }

                contact.CongTy = _contact.CongTy;

                contact.DichVu = _contact.DichVu;
                contact.contents = _contact.contents;
                db.Contacts.Add(contact);
                db.SaveChanges();

              
                try
                {
                    List<string> lstEmailTo = new List<string>();
                
                    lstEmailTo.Add(contact.Email);
                    string emailBody = contact.Name; 

                    string Subject = HttpContext.Request.Url.Host +  "- Yêu cầu demo";

                    EmailService emailservice = new EmailService();
                    var EmailNhanCTY = System.Configuration.ConfigurationSettings.AppSettings["EmailNhan"];
                    var Google_email = System.Configuration.ConfigurationSettings.AppSettings["Google_email"];
                    var Google_email_Pass = System.Configuration.ConfigurationSettings.AppSettings["Google_email_Pass"];

                    string body = "";
                    StreamReader sr = new StreamReader(Server.MapPath("~/FileTemplate/template_request_Demo.html"));
                    body = sr.ReadToEnd();
                    string NgayTao = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
                    body = string.Format(body, contact.Name, NgayTao,  contact.Email, contact.ChucVu,
                        contact.CongTy, contact.Address,  contact.LinhVuc, contact.BangGiaChiTiet,contact.Phone);
                


                    var ruslt = emailservice.SendEmail(lstEmailTo, Subject, EmailNhanCTY, body, Google_email, Google_email_Pass);
                }
                catch (Exception ex)
                {

                }

                return RedirectToAction("Thanks");
            }

            var lstBaiVietGioiThieu = db.Abouts.AsNoTracking().Where(x => x.AppID == this.AppID).ToList();
            ViewBag.GioiThieu = lstBaiVietGioiThieu;

            return View(_contact);
        }

        [HttpGet]
        public ActionResult DangKy()
        {
            ContactModels contact = new ContactModels();
            return View(contact);
        }

        [HttpPost]
        public ActionResult DangKy(ContactModels _contact, SubEmail sub)
        {
            if (ModelState.IsValid)
            {
                Contact contact = new Contact();
                contact.Name = _contact.Name;
                contact.Email = _contact.Email;
                contact.Phone = _contact.Phone;
                contact.contents = _contact.contents;
                contact.DichVu = _contact.DichVu;
                contact.MoTaDichVu = _contact.MoTaDichVu;
                contact.Address = _contact.Address;
                contact.Fax = _contact.CongTy;
                contact.TypeContact = "E_DangKy";
                contact.Status = "E_DangKyMoi";
                contact.CreateDate = DateTime.Now;
                string content = "Thông tin người liên hệ :" + "<br/>" + "Họ tên :" + _contact.Name + "<br/>" + "Email :" + _contact.Email + "<br/>" + "Nội dụng :" + _contact.contents;
                db.Contacts.Add(contact);
                db.SaveChanges();

                try
                {
                    SendEmail mail = db.SendEmails.FirstOrDefault();
                    // emailserice.SendEmail1(mail.Subject, mail.Emailfrom, contact.Title, content, mail.Passmail, mail.Servermail);
                }
                catch (Exception ex)
                {
                }
            }
            return View(_contact);
        }

        [HttpGet]
        public ActionResult HopTac()
        {
            ContactModels contact = new ContactModels();
            return View(contact);
        }

        [HttpPost]
        public ActionResult HopTac(ContactModels _contact, SubEmail sub)
        {
            if (ModelState.IsValid)
            {
                Contact contact = new Contact();
                contact.Name = _contact.Name;
                contact.Email = _contact.Email;
                contact.Phone = _contact.Phone;
                contact.contents = _contact.contents;
                contact.DichVu = _contact.DichVu;
                contact.MoTaDichVu = _contact.MoTaDichVu;
                contact.Address = _contact.Address;
                contact.Fax = _contact.CongTy;
                contact.TypeContact = "E_HopTac";
                contact.Status = "E_DangKyMoi";
                contact.CreateDate = DateTime.Now;
                string content = "Thông tin người liên hệ :" + "<br/>" + "Họ tên :" + _contact.Name + "<br/>" + "Email :" + _contact.Email + "<br/>" + "Nội dụng :" + _contact.contents;
                db.Contacts.Add(contact);
                db.SaveChanges();

                try
                {
                    SendEmail mail = db.SendEmails.FirstOrDefault();
                    // emailserice.SendEmail1(mail.Subject, mail.Emailfrom, contact.Title, content, mail.Passmail, mail.Servermail);
                }
                catch (Exception ex)
                {
                }
            }
            return View(_contact);
        }

        //public ActionResult About(int id)
        //{
        //    AboutCategory getValueCate = db.AboutCategories.Where(a => a.AboutID == id).FirstOrDefault();
        //    return View(getValueCate);
        //}
        public ActionResult About()
        {
            var about = db.Abouts.Where(m => m.NameAlias == "SMS_ThuongHieu" && m.LanguageId.Contains("vi")).FirstOrDefault();
            ViewBag.Title = about.Name;
            return View("_IndexAbout", about);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (db != null))
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Maps()
        {
            return View();
            //return View(db.Contacts.ToList());
        }

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

        public ActionResult GetContact()
        {
            ViewBag.contact = db.Abouts.Where(m => m.NameAlias == "LienHe").Select(m => m.Description).FirstOrDefault();

            return PartialView();
        }

        public ActionResult Thanks()
        {
            var lstBaiVietGioiThieu = db.Abouts.AsNoTracking().Where(x => x.AppID == this.AppID).ToList();
            ViewBag.GioiThieu = lstBaiVietGioiThieu;
            return View();
        }
    }
}