using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BlueDataWeb.Models.Entites;
using BlueDataWeb.Models.Service;
using BlueDataWeb.Models.Enum;
 



namespace BlueDataWeb.Controllers
{
    public class UserController : Controller
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();
        EmailService emailservice = new EmailService();
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View(db.Users.ToList());

        }

        //
        // GET: /User/Details/5
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
        public JsonResult DistrictList(int Id)
        {

            var district = from s in db.Districts
                           where s.MaTinhThanh == Id
                           select s;

            return Json(new SelectList(district.ToList(), "ID", "TenQuanHuyen"), JsonRequestBehavior.AllowGet);
        }
        public JsonResult WardList(int Id)
        {
            var ward = from s in db.Wards
                       where s.MaQuanHuyen == Id
                       select s;

            return Json(new SelectList(ward.ToList(), "WardID", "TenPhuongXa"), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult RegisterTop()
        {
            ViewBag.City_Province = new SelectList(db.Provices.ToList(), "ID", "TenTinhThanh");
            //ViewBag.ProviceID = new SelectList(db.Provices.ToList(), "ID", "TenTinhThanh");

            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult RegisterTop(User user, string Birthday)
        {
            User usertest = db.Users.Where(m => m.Email.Contains(user.Email.Trim())).FirstOrDefault();

            string b = Birthday;
            user.Birthday = ConvertStringToDateTime_Split(b);


            if (user.Birthday == null || user.Phone == null || user.Name == null || user.Password == null || user.Email == null || usertest != null)
            {
                if (user.Birthday == null)
                    ViewBag.errorBirthday = "Vui lòng nhập Ngày sinh";
                if (user.Phone == null)
                    ViewBag.errorPhone = "Vui lòng nhập số điện thoại";
                if (user.Name == null)
                    ViewBag.errorName = "Vui lòng nhập Tên";
                if (user.Email == null)
                    ViewBag.errorEmail = " Vui lòng nhập Email";
                if (user.Password == null)
                    ViewBag.errorPassword = " Vui lòng nhập Mật khẩu";
                if (usertest != null)
                    ViewBag.errorEmaildoub = " Email đã tồn tại";

                ViewBag.City_Province = new SelectList(db.Provices.ToList(), "ID", "TenTinhThanh", user.City_Province);
                return View(user);
            }
            user.Password = md5(user.Password);
            user.Status = false;
            user.IsActive = true;
            user.CodeActive = "?" + md5(user.Email);
            db.Users.Add(user);
            db.SaveChanges();
            Session["Username"] = user.Email;
            Session["User"] = user.Name;
            return RedirectToAction("Index", "Home");
            ViewBag.City_Province = new SelectList(db.Provices.ToList(), "ID", "TenTinhThanh", user.City_Province);
            return View(user);
        }

        [HttpGet]
        public ActionResult ViewOrder()
        {
            if (Session["Username"] == null)
                return RedirectToAction("login", "User");

            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Register(User user, FormCollection collection, SubEmail subemail)
        {
            
            User usertest = db.Users.Where(m => m.Email.Contains(user.Email.Trim())).FirstOrDefault();
            SubEmail emailtest = db.SubEmails.Where(m => m.Email.Contains(user.Email.Trim())).FirstOrDefault();
            if (user.Name == null
               || user.Password == null || user.Email == null || usertest != null)
            {
                if (user.Name == null)
                    ViewBag.errorName = "Vui lòng nhập Tên";

                if (user.Password == null)
                    ViewBag.errorPassword = " Vui lòng nhập Mật khẩu";

                if (user.Email == null)
                    ViewBag.errorEmail = " Vui lòng nhập Email";

                if (usertest != null)
                    ViewBag.errorEmaildoub = " Email đã tồn tại";

                return View(user);

            }
            if (collection["sube"].ToString() != "false" && emailtest ==null)
            {
                
                db.SubEmails.Add(subemail);
                db.SaveChanges();
            }

            if (ModelState.IsValid)
            {
                user.Password = md5(user.Password);
                user.Status = false;
                user.IsActive = true;
                user.CodeActive = "?" + md5(user.Email);
                db.Users.Add(user);
                db.SaveChanges();
                Session["Username"] = user.Email;
                Session["User"] = user.Name;
                if (Session["URL"] != null)
                {
                    string decodedUrl = "";
                    if (!string.IsNullOrEmpty(Session["URL"].ToString()))
                        decodedUrl = Server.UrlDecode(Session["URL"].ToString());

                    if (Url.IsLocalUrl(decodedUrl) && (decodedUrl == "/ShoppingCart?Length=12" || decodedUrl == "/ShoppingCart/Index"))
                        return Redirect("/Payment/AddressAndPayment");
                }


                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        public ActionResult login(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) && Request.UrlReferrer != null)
                returnUrl = Server.UrlEncode(Request.UrlReferrer.PathAndQuery);

            if (Url.IsLocalUrl(returnUrl) && !string.IsNullOrEmpty(returnUrl))
            {
                ViewBag.ReturnURL = returnUrl;
            }

            Session["URL"] = ViewBag.ReturnURL = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult login(User user, string returnUrl )
        {
            User usertest = db.Users.Where(m => m.Email == user.Email).FirstOrDefault();
            if (usertest == null)
            {
                ViewBag.error = "Emai không tồn tại";
            }
            else
            {
                if (usertest.Password != md5(user.Password))
                {
                    ViewBag.error = "Vui lòng nhập lại mật khẩu";
                }
                else
                {
                    // check if cookie exists and if yes update
                    HttpCookie existingCookie = Request.Cookies["userName"];
                    if (existingCookie != null)
                    {
                        // force to expire it
                        existingCookie.Value = usertest.Email;
                        existingCookie.Expires = DateTime.Now.AddHours(-20);
                    }

                    // create a cookie
                    HttpCookie newCookie = new HttpCookie("userName", usertest.Email);
                    newCookie.Expires = DateTime.Today.AddMonths(12);
                    Response.Cookies.Add(newCookie);

                    Session["Username"] = user.Email;
                    Session["User"] = usertest.Name;
                    
                    string decodedUrl = "";
                    if (!string.IsNullOrEmpty(returnUrl))
                        decodedUrl = Server.UrlDecode(returnUrl);
                    
                    if (Url.IsLocalUrl(decodedUrl) && (decodedUrl == "/ShoppingCart/Index?Length=12" || decodedUrl =="/ShoppingCart/Index"))
                        return Redirect("/Payment/AddressAndPayment");
                    else if(decodedUrl !=null && decodedUrl !="")
                        return Redirect(decodedUrl);
                    else
                        return RedirectToAction("Index", "Home");

                }

            }
            ViewBag.ReturnURL = returnUrl;

            return View(user);
        }

        [HttpGet]
        public ActionResult logout()
        {
            Session["Username"] = null;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult UserInformation()
        {
            if (Session["Username"] != null)
            {
                string email = Session["Username"].ToString();
                User usertest = db.Users.Where(m => m.Email == email).FirstOrDefault();
                ViewBag.Provices = new SelectList(db.Provices.ToList(), "ID", "TenTinhThanh", usertest.City_Province);
                ViewBag.Districts = new SelectList(db.Districts.Where(m => m.MaTinhThanh == usertest.City_Province).ToList(), "ID", "TenQuanHuyen", usertest.District);
                ViewBag.Wards = new SelectList(db.Wards.Where(m => m.MaQuanHuyen == usertest.District).ToList(), "WardID", "TenPhuongXa", usertest.Ward);
                string birthday = usertest.Birthday.ToString();
                return View(usertest);
                
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult UserInformation(User user, string Birthday)
        {
            user.Birthday = ConvertStringToDateTime_Split(Birthday);
            ModelState.Remove("Birthday");
            if (user.Phone == null || user.Name == null || user.Email == null)
            {
                if (user.Birthday == null)
                    ViewBag.errorBirthday = "Vui lòng nhập Ngày sinh";
                if (user.Phone == null)
                    ViewBag.errorPhone = "Vui lòng nhập số điện thoại";
                if (user.Name == null)
                    ViewBag.errorName = "Vui lòng nhập Tên";
                if (user.Email == null)
                    ViewBag.errorEmail = " Vui lòng nhập Email";


                ViewBag.City_Province = new SelectList(db.Provices.ToList(), "ID", "TenTinhThanh", user.City_Province);
                ViewBag.District = new SelectList(db.Districts.ToList(), "ID", "TenQuanHuyen", user.District);
                ViewBag.Ward = new SelectList(db.Wards.ToList(), "WardID", "TenPhuongXa", user.Ward);
                ViewBag.Gender = new SelectList((new DataModel()).getsex(), "ID", "Value", user.Gender);

                return View(user);
            }
            BlueDataWebEntities dbnew = new BlueDataWebEntities();
            if (user.Password != null)
            {
                user.Password = md5(user.Password);
            }
            if (user.Password == null || user.Password == "")
            {
                User usertest = dbnew.Users.Where(m => m.Email == user.Email).FirstOrDefault();
                user.Password = usertest.Password;

            }

           
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            

            ViewBag.City_Province = new SelectList(dbnew.Provices.ToList(), "ID", "TenTinhThanh", user.City_Province);
            ViewBag.District = new SelectList(dbnew.Districts.ToList(), "ID", "TenQuanHuyen", user.District);
            ViewBag.Ward = new SelectList(dbnew.Wards.ToList(), "WardID", "TenPhuongXa", user.Ward);
            ViewBag.Gender = new SelectList((new DataModel()).getsex(), "ID", "Value", user.Gender);
            return View();
        }
        public ActionResult PassRestore()
        {
            //User usertest2 = db.Users.Where(m => m.Email == ).FirstOrDefault();
            //PopulateDepartmentsDropDownList(course.DepartmentID);
            return View();
        }
        [HttpPost]
        public ActionResult PassRestore(User user)
        {
            User usertest = db.Users.Where(m => m.Email == user.Email).FirstOrDefault();
            if (user.Email == null || usertest == null)
            {
                if (user.Email == null)
                {
                    ViewBag.erroremail = "Vui lòng nhập Email ";
                    return View();
                }
                if (usertest == null)
                    ViewBag.erroremail2 = "Email không tồn tại";
                return View();
            }
            if (ModelState.IsValid)
            {
                SendEmail sendemail = db.SendEmails.Where(m => m.Name == "PassRestore").FirstOrDefault();
                string pass = Random();
                string emailto = user.Email;
                string body = sendemail.Body + pass;
                emailservice.SendEmailPass(sendemail, emailto, body);
                //string emailto = ConfigurationManager.AppSettings["emailto"];
                //string subject = sendemail.Subject;
                //string body = sendemail.Body + pass;
                //bool sendmail = emailservice.SendMail_By_Gmail(emailto, subject, body);
                usertest.Password = md5(pass);
                db.Entry(usertest).State = EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = "Khôi phục thành công! Mời bạn vào Email để lấy mật khẩu mới";
                return RedirectToAction("Index", "Home");
            }
            
            return View(user);
        }

        public string Random()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            string rd = finalString;
            return rd;
        }
        public static DateTime? ConvertStringToDateTime_Split(string _str)
        {
            if (string.IsNullOrEmpty(_str)) return null;

            DateTime dtReturn = DateTime.Now;

            string[] arrDate = _str.Trim().Split('/');
            string year = arrDate[2];
            string month = arrDate[1];
            string day = arrDate[0];
            int hour = 0;
            int min = 0;
            int sec = 0;
            string[] arrHour;

            if (year.Contains(':'))
            {
                arrHour = year.Split(' ');
                year = arrHour[0];
                string strHour = arrHour[1];
                arrHour = strHour.Split(':');
                if (arrHour.Count() == 3)
                {
                    hour = Convert.ToInt16(arrHour[0]);
                    min = Convert.ToInt16(arrHour[1]);
                    sec = Convert.ToInt16(arrHour[2]);
                }
            }

            try
            {
                dtReturn = new DateTime(Convert.ToInt16(year), Convert.ToInt16(month), Convert.ToInt16(day));//, hour, min, sec);
            }
            catch
            {
                return null;
            }

            return dtReturn;
        }


    }
}
