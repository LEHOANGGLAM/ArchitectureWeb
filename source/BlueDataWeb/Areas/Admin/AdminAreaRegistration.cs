using BlueDataWeb.Models.CustomsModel;
using System.Web.Mvc;

namespace BlueDataWeb.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //context.MapRoute(
            //    "Admin_default",
            //    "Admin/{controller}/{action}/{id}",
            //    new { controller = "Home", action = "Login", id = UrlParameter.Optional }
            //);

            context.MapRoute(
                "Admin_default",
                ""+  GlobalVariables.PathFolderName+"/{controller}/{action}/{id}",
                new { controller = "Account", action = "LogOn", id = UrlParameter.Optional }
            );
        }
    }
}
