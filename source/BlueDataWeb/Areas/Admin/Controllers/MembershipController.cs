using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueDataWeb.Models.Entites;


namespace BlueDataWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, AdminMember")]
    public class MembershipController : Controller
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();

        //
        // GET: /Admin/Membership/

        public ActionResult Index()
        {
            ViewBag.lstrole = db.UsersInRoles.ToList();
            return View(db.Memberships.ToList());
        }

        //
        // GET: /Admin/Membership/Details/5

        public ActionResult Details(int id = 0)
        {
            var lstuserrole = db.UsersInRoles.ToList();
            ViewBag.lstuserrole = lstuserrole;
            Membership membership = db.Memberships.Find(id);
            ViewBag.userid = membership.UserId;

            if (membership == null)
            {
                return HttpNotFound();
            }
            return View(membership);
        }

        //
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
        // GET: /Admin/Membership/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Membership/Create

        [HttpPost]
        public ActionResult Create(Membership membership, UsersInRole role)
        {
            var usernametest = db.Memberships.Where(m => m.Username == membership.Username).FirstOrDefault();
            var emailtest = db.Memberships.Where(m => m.Email == membership.Email).FirstOrDefault();
            if (usernametest != null)
            {
                ViewBag.usernametest = "Tên đăng nhập đã tồn tại";
                return View();
            }
            if (emailtest != null)
            {
                ViewBag.emailtest = "Email đã tồn tại";
                return View();
            }

            if (ModelState.IsValid)
            {
                User usertest = db.Users.Where(m => m.Email == membership.Email).FirstOrDefault();
                if (usertest != null)
                {
                    usertest.Password = membership.Password;
                    db.Entry(usertest).State = EntityState.Modified;
                }
                else
                {
                    User user = new User()
                    {
                        Email = membership.Email,
                        Password = membership.Password,
                        Name = membership.Username
                    };

                    db.Users.Add(user);
                }

                membership.Password = md5(membership.Password);
                db.Memberships.Add(membership);
                db.SaveChanges();
                var roles = db.Roles.ToList();
                role.UserId = membership.UserId;
                foreach (var ro in roles)
                {
                    var chk = Request.Form["chk" + ro.RoleId];
                    if (chk == "on")
                    {
                        role.RoleId = ro.RoleId;

                        if (ModelState.IsValid)
                        {
                            db.UsersInRoles.Add(role);
                            db.SaveChanges();

                        }

                    }
                }
                return RedirectToAction("Index");
            }

            return View(membership);
        }

        //
        // GET: /Admin/Membership/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Membership membership = db.Memberships.Find(id);
            ViewBag.userid2 = membership.UserId;
            var lstrole = db.UsersInRoles.Where(m => m.UserId == membership.UserId).ToList();
            ViewBag.lstrole = lstrole;
            if (membership == null)
            {
                return HttpNotFound();
            }
            return View(membership);
        }

        //
        // POST: /Admin/Membership/Edit/5

        [HttpPost]
        public ActionResult Edit(Membership membership, UsersInRole role)
        {
            int i = membership.UserId;
            //rm
            //BlueDataWebEntities dbnew2 = new BlueDataWebEntities();
            var uirole = db.UsersInRoles.Where(d => d.UserId == i).ToList();
            foreach (var u in uirole)
            {
                db.UsersInRoles.Remove(u);
                db.SaveChanges();
            }
            BlueDataWebEntities dbnew = new BlueDataWebEntities();

            string s = membership.Password;
            Membership membertest = dbnew.Memberships.Where(m => m.Username == membership.Username).FirstOrDefault();
            if (membership.Password != null || membership.Password == "")
            {
                membership.Password = md5(membership.Password);
            }
            if (membership.Password == null || membership.Password == "")
            {
                membership.Password = membertest.Password;
            }
            membership.LastLoginDate = membertest.LastLoginDate;
            if (ModelState.IsValid)
            {
                User usertest = db.Users.Where(m => m.Email == membership.Email).FirstOrDefault();
                if (usertest != null)
                {
                    usertest.Password = membership.Password;
                    db.Entry(usertest).State = EntityState.Modified;
                }
                else
                {
                    User user = new User()
                    {
                        Email = membership.Email,
                        Password = membership.Password,
                        Name = membership.Username
                        
                    };

                    db.Users.Add(user);
                }

                db.Entry(membership).State = EntityState.Modified;
                db.SaveChanges();
                var roles = db.Roles.ToList();
                role.UserId = membership.UserId;
                foreach (var ro in roles)
                {
                    var chk = Request.Form["check" + ro.RoleId];
                    if (chk == "on")
                    {
                        role.RoleId = ro.RoleId;

                        if (ModelState.IsValid)
                        {
                            db.UsersInRoles.Add(role);
                            db.SaveChanges();

                        }

                    }
                }
                return RedirectToAction("Index");
            }
            return View(membership);
        }

        //
        // GET: /Admin/Membership/Delete/5

        public ActionResult MemberHistory()
        {
            var lstmember = db.Memberships.Where(m => m.LastLoginDate != null).OrderByDescending(m => m.LastLoginDate).ToList();
            ViewBag.lstRole = db.UsersInRoles.Include(m=>m.Role).ToList();
            return PartialView(lstmember);
        }

        public ActionResult Delete(int id = 0)
        {
            var lstuserrole = db.UsersInRoles.ToList();
            ViewBag.lstuserrole = lstuserrole;
            Membership membership = db.Memberships.Find(id);
            ViewBag.userid = membership.UserId;
            if (membership == null)
            {
                return HttpNotFound();
            }
            return View(membership);
        }

        //
        // POST: /Admin/Membership/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Membership membership = db.Memberships.Find(id);
            var uir = db.UsersInRoles.Where(d => d.UserId == membership.UserId).ToList();
            foreach (var u in uir)
            {
                db.UsersInRoles.Remove(u);
                db.SaveChanges();
            }

            db.Memberships.Remove(membership);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Profile()
        {

            Membership member = db.Memberships.Where(m => m.Username == User.Identity.Name).FirstOrDefault();
            return View(member);
        }
        [HttpPost]
        public ActionResult Profile(Membership membership)
        {
            BlueDataWebEntities dbnew = new BlueDataWebEntities();
            Membership membertest = dbnew.Memberships.Where(m => m.Username == User.Identity.Name).FirstOrDefault();
            if (membership.Password != null || membership.Password == "")
            {
                membership.Password = md5(membership.Password);
            }
            if (membership.Password == null || membership.Password == "")
            {
                membership.Password = membertest.Password;
            }
            membership.LastLoginDate = membertest.LastLoginDate;
            if (ModelState.IsValid)
            {
                db.Entry(membership).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();

        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}