using System.Web.Optimization;

namespace BlueDataWeb
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-{version}.js",
                       "~/Scripts/jquery.validate*",

                       "~/Scripts/jquery.dotdotdot.js",

                       "~/Scripts/bootstrap.js",
                       "~/Scripts/respond.js",

                      "~/Scripts/jquery.signalR-1.1.3.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/css/font-awesome.css",
                      "~/Content/Site.css"));

            #region Theme BHDT_V2

            bundles.Add(new ScriptBundle("~/bundles/BHDT_V2_js").Include(
                  "~/Themes/BHDT_V2/js/jquery-1.9.1.js",
                  "~/Themes/BHDT_V2/js/jquery.smartmenus.js",
                  "~/Themes/BHDT_V2/css/owl.carousel/owl.carousel.js",
                  "~/Themes/BHDT_V2/js/jquery.dotdotdot.js",
                  "~/Themes/BHDT_V2/js/jquery.lazyscript.js",
                  "~/Themes/BHDT_V2/js/main.js"));

            bundles.Add(new StyleBundle("~/bundles/BHDT_V2_css").Include(
                 "~/Themes/BHDT_V2/css/font-awesome.min.css",
                 "~/Themes/BHDT_V2/css/framework.css",
                 "~/Themes/BHDT_V2/css/layout.css",
                 "~/Themes/BHDT_V2/css/menu_sm-core-css.css",
                 "~/Themes/BHDT_V2/css/menu_sm-blue.css",
                 "~/Themes/BHDT_V2/css/owl.carousel/owl.carousel.css",
                 "~/Themes/BHDT_V2/css/owl.carousel/owl.theme.css"));

            #endregion Theme BHDT_V2

            #region Theme Bluedata

            bundles.Add(new ScriptBundle("~/bundles/BlueData_js").Include(
                  "~/Themes/BlueData/js/jquery-1.9.1.js",
                  "~/Themes/BlueData/js/jquery.smartmenus.js",
                  "~/Themes/BlueData/css/owl.carousel/owl.carousel.js",
                  "~/Themes/BlueData/js/jquery.dotdotdot.js",
                  "~/Themes/BlueData/js/jquery.lazyscript.js",
                  "~/Themes/BlueData/js/main.js"));

            bundles.Add(new StyleBundle("~/bundles/BlueData_css").Include(

                 "~/Themes/BlueData/css/framework.css",
                 "~/Themes/BlueData/css/layout.css",
                 "~/Themes/BlueData/css/menu_sm-core-css.css",
                 "~/Themes/BlueData/css/menu_sm-blue.css",
                 "~/Themes/BlueData/css/owl.carousel/owl.carousel.css",
                 "~/Themes/BlueData/css/owl.carousel/owl.theme.css"));

            #endregion Theme Bluedata

            #region Theme Dữ liệu biến đổi

            bundles.Add(new ScriptBundle("~/bundles/DLBD_js").Include(
                  "~/Themes/DuLieuBD/js/jquery-1.9.1.js",
                  "~/Themes/DuLieuBD/js/jquery.smartmenus.js",
                  "~/Themes/DuLieuBD/css/owl.carousel/owl.carousel.js",
                  "~/Themes/DuLieuBD/js/jquery.dotdotdot.js",
                  "~/Themes/DuLieuBD/js/jquery.lazyscript.js",
                  "~/Themes/DuLieuBD/js/main.js"));

            bundles.Add(new StyleBundle("~/bundles/DlBD_css").Include(

                 "~/Themes/DuLieuBD/css/framework.css",
                 "~/Themes/DuLieuBD/css/layout.css",
                 "~/Themes/DuLieuBD/css/menu_sm-core-css.css",
                 "~/Themes/DuLieuBD/css/menu_sm-blue.css",
                 "~/Themes/DuLieuBD/css/owl.carousel/owl.carousel.css",
                 "~/Themes/DuLieuBD/css/owl.carousel/owl.theme.css"));

            #endregion Theme Dữ liệu biến đổi

            #region Theme xác thực điện tử

            bundles.Add(new ScriptBundle("~/bundles/XacThucDT_js").Include(
                  "~/Themes/XacThucDT/js/jquery-1.9.1.js",
                  "~/Themes/XacThucDT/js/jquery.smartmenus.js",
                  "~/Themes/XacThucDT/css/owl.carousel/owl.carousel.js",
                  "~/Themes/XacThucDT/js/jquery.dotdotdot.js",
                  "~/Themes/XacThucDT/js/jquery.lazyscript.js",
                  "~/Themes/XacThucDT/js/main.js"));

            bundles.Add(new StyleBundle("~/bundles/XacThucDT_css").Include(

                 "~/Themes/XacThucDT/css/framework.css",
                 "~/Themes/XacThucDT/css/layout.css",
                 "~/Themes/XacThucDT/css/menu_sm-core-css.css",
                 "~/Themes/XacThucDT/css/menu_sm-blue.css",
                 "~/Themes/XacThucDT/css/owl.carousel/owl.carousel.css",
                 "~/Themes/XacThucDT/css/owl.carousel/owl.theme.css"));

            #endregion Theme xác thực điện tử

            #region Theme temnhanchonggia.com

            bundles.Add(new ScriptBundle("~/bundles/TemNhanChongGia_js").Include(
                  "~/Themes/TemNhanChongGia/js/jquery-1.9.1.js",
                  "~/Themes/TemNhanChongGia/js/jquery.smartmenus.js",
                  "~/Themes/TemNhanChongGia/css/owl.carousel/owl.carousel.js",
                  "~/Themes/TemNhanChongGia/js/jquery.dotdotdot.js",
                  "~/Themes/TemNhanChongGia/js/jquery.lazyscript.js",
                  "~/Themes/TemNhanChongGia/js/main.js"));

            bundles.Add(new StyleBundle("~/bundles/TemNhanChongGia_css").Include(

                 "~/Themes/TemNhanChongGia/css/framework.css",
                 "~/Themes/TemNhanChongGia/css/layout.css",
                 "~/Themes/TemNhanChongGia/css/menu_sm-core-css.css",
                 "~/Themes/TemNhanChongGia/css/menu_sm-blue.css",
                 "~/Themes/TemNhanChongGia/css/owl.carousel/owl.carousel.css",
                 "~/Themes/TemNhanChongGia/css/owl.carousel/owl.theme.css"));

            #endregion Theme temnhanchonggia.com
        }
    }
}