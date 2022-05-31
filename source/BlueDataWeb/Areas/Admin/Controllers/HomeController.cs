using BlueDataWeb.Models.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using System.Data;
using BlueDataWeb.Models.CustomsModel;

namespace BlueDataWeb.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        BlueDataWebEntities db = new BlueDataWebEntities();
        [HttpPost]
        public ActionResult Upload()
        {

            HttpPostedFileBase upl = Request.Files["image"];
            System.Drawing.Image imgInput = System.Drawing.Image.FromStream(upl.InputStream);
            String name = DateTime.Now.Ticks + upl.FileName;
            String path = "/Images/News/" + name;
            String filename = Server.MapPath(path);
            upl.SaveAs(filename);
            String result = "/Images/News/" + name ;
            return Content(result);

        }
        //
        // GET: /Admin/Home/
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
        }
        public ActionResult Index()
        {
            if (User.Identity.Name == "")
            {
                return Redirect("~/" + GlobalVariables.PathFolderName);
            }
            else
            {
                return View();
            }

        }
    }
}
