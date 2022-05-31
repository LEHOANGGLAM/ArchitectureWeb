using System.Configuration;
using System.Web.Mvc;

namespace BlueDataWeb.Helpers
{
    public class CustomViewEngine : RazorViewEngine
    {
        public CustomViewEngine()
        {
            var themeName = ConfigurationManager.AppSettings["ThemeName"];

            ViewLocationFormats = new string[] { "~/Themes/" + themeName + "/Views/{1}/{0}.cshtml", "~/Themes/" + themeName + "/Views/Shared/{0}.cshtml", "~/Themes/" + themeName + "/Views/{1}/{0}.cshtml", "~/Themes/" + themeName + "/Views/Shared/{0}.cshtml" };
            PartialViewLocationFormats = new string[] { "~/Themes/" + themeName + "/Views/{1}/{0}.cshtml", "~/Themes/" + themeName + "/Views/Shared/{0}.cshtml", "~/Themes/" + themeName + "/Views/{1}/{0}.cshtml", "~/Themes/" + themeName + "/Views/Shared/{0}.cshtml" };
        }
    }
}