//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using ExpressBD.Models;
//namespace ExpressBD.Controllers
//{
//    public class AboutController : Controller
//    {
//        ExpressBDEntities db = new ExpressBDEntities();
//        //
//        // GET: /About/

//        public ActionResult Index()
//        {
//            var about = db.Settings.Where(m => m.Key == "SMS_Brandname" && m.LanguageId.Contains("vi")).FirstOrDefault();
//            ViewBag.Description = TempData["Description"];
//            ViewBag.Keywords = TempData["Keywords"];
//            ViewBag.Title = "Dịch vụ SMS";
//            return View(about);
//        }
//        [HttpGet]
//        public ActionResult Detail(string type)
//        {
//            ContactModels contact = new ContactModels();
//            var about = db.Settings.Where(m => m.Key == type && m.LanguageId.Contains("vi")).FirstOrDefault();
//            ViewBag.Description = about.MetaDescription;
//            ViewBag.Keywords = about.MetaKeywords;
//            ViewBag.Title = about.Name;
//            ViewBag.objDichVu = about;
//            return View(contact);
//        }

//        [HttpGet]
//        public ActionResult BaiViet(string type)
//        {
//            var about = db.Settings.Where(m => m.Key == type && m.LanguageId.Contains("vi")).FirstOrDefault();
//            ViewBag.Description = about.MetaDescription;
//            ViewBag.Keywords = about.MetaKeywords;
//            ViewBag.Title = about.Name;
//            return View(about);
//        }
//        [HttpPost]
//        public ActionResult Detail(ContactModels _contact, SubEmail sub, string type)
//        {
//            if (ModelState.IsValid)
//            {
//                string DichVu = string.Empty;
//                string MoTaDichVu = string.Empty;
//                Contact contact = new Contact();
//                contact.Name = _contact.Name;
//                contact.Email = _contact.Email;
//                contact.Phone = _contact.Phone;
//                contact.contents = _contact.contents;
//                contact.Address = _contact.Address;
//                contact.TypeContact = "E_DichVu";
//                contact.Status = "E_DangKyMoi";
//                contact.CreateDate = DateTime.Now;

//                //dịch vụ
//                foreach (var ro in new DataModel().getDichVu())
//                {
//                    var chk = Request.Form[ro.ID];
//                    if (chk == "on")
//                    {
//                        DichVu += ro.ID + ";";
//                    }
//                }
//                //mô tả dịch vụ
//                foreach (var ro in new DataModel().getMoTaDichVu())
//                {
//                    var chk = Request.Form[ro.ID];
//                    if (chk == "on")
//                    {
//                        MoTaDichVu += ro.ID + ";";
//                    }
//                }

//                contact.DichVu = DichVu;
//                contact.MoTaDichVu = MoTaDichVu;
//                string content = "Thông tin người liên hệ :" + "<br/>" + "Họ tên :" + _contact.Name + "<br/>" + "Email :" + _contact.Email + "<br/>" + "Nội dụng :" + _contact.contents;
//                db.Contacts.Add(contact);
//                db.SaveChanges();

//                try
//                {
//                    SendEmail mail = db.SendEmails.FirstOrDefault();
//                    // emailserice.SendEmail1(mail.Subject, mail.Emailfrom, contact.Title, content, mail.Passmail, mail.Servermail);
//                }
//                catch (Exception ex)
//                {

//                }
//                return RedirectToAction("Detail");
//            }

//            var about = db.Settings.Where(m => m.Key == type && m.LanguageId.Contains("vi")).FirstOrDefault();
//            ViewBag.Description = about.MetaDescription;
//            ViewBag.Keywords = about.MetaKeywords;
//            ViewBag.Title = about.Name;
//            ViewBag.objDichVu = about;
//            return View(_contact);
//        }

//    }
//}
