using AutoMapper;
using BlueDataWeb.Controllers;
using BlueDataWeb.Helpers;
using BlueDataWeb.Models.CustomsModel;
using BlueDataWeb.Models.Entites;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BlueDataWeb
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode,
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
         name: "trang-chu",
         url: "trang-chu",
         defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
        );

            routes.MapRoute(
                 name: "KienTrucList",
                 url: "kien-truc/{title}-{id}",
                 defaults: new { controller = "Architecture", action = "List", id = UrlParameter.Optional }
              );

            routes.MapRoute(
               name: "KienTrucDetail",
               url: "kien-truc/{cate}/{title}-{id}",
               defaults: new { controller = "Architecture", action = "Detail", id = UrlParameter.Optional }
            );

            #region Gioi thieu

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
            name: "gioithieu",
            url: "gioi-thieu",
            defaults: new { controller = "GioiThieu", action = "Index" }
             );

            routes.MapRoute(
                name: "gioithieudetail",
                url: "gioi-thieu/{title}-{id}",
                defaults: new { controller = "GioiThieu", action = "Detail", id = UrlParameter.Optional }
            );

            #endregion Gioi thieu

            #region chucnang

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "chucnang",
                url: "chuc-nang",
                defaults: new { controller = "ChucNang", action = "Index" }
           );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
            name: "chucnangdetail",
            url: "chuc-nang/{title}-{id}",
            defaults: new { controller = "ChucNang", action = "Details" }
             );

            #endregion chucnang

            #region Trienkhai

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
            name: "trienkhai",
            url: "trien-khai",
            defaults: new { controller = "TrienKhai", action = "Index" }
           );

            routes.MapRoute(
          name: "trienkhaidetail",
          url: "trien-khai/{title}-{id}",
          defaults: new { controller = "TrienKhai", action = "Details", id = UrlParameter.Optional }
       );

            #endregion Trienkhai

            #region Chi phí

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
            name: "chiphi",
            url: "chi-phi",
            defaults: new { controller = "ChiPhi", action = "Index" }
           );

            #endregion Chi phí

            #region GiaiPhap

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "giaiphap",
                url: "giai-phap",
                defaults: new { controller = "GiaiPhap", action = "Index" }
           );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
            name: "giaiphapdetail",
            url: "giai-phap/{title}-{id}",
            defaults: new { controller = "GiaiPhap", action = "Details" }
             );

            #endregion GiaiPhap

            #region Sản phẩm

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "sanpham",
                url: "san-pham",
                defaults: new { controller = "SanPham", action = "Index" }
           );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
            name: "sanphamdetail",
            url: "san-pham/{title}-{id}",
            defaults: new { controller = "SanPham", action = "Details" }
             );

            #endregion Sản phẩm

            #region DichVu

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "dichvu",
                url: "dich-vu",
                defaults: new { controller = "DichVu", action = "Index" }
           );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
            name: "dichvudetail",
            url: "dich-vu/{title}-{id}",
            defaults: new { controller = "DichVu", action = "Details" }
             );

            #endregion DichVu

            #region Outsourcing

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Outsourcing",
                url: "e-Outsourcing",
                defaults: new { controller = "Outsourcing", action = "Index" }
           );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
            name: "Outsourcingdetail",
            url: "e-Outsourcing/{title}-{id}",
            defaults: new { controller = "Outsourcing", action = "Details" }
             );

            #endregion Outsourcing

            #region Bảng giá

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "bangia",
                url: "bang-gia",
                defaults: new { controller = "BangGia", action = "Index" }
           );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
            name: "bangiaDetail",
            url: "bang-gia/{title}-{id}",
            defaults: new { controller = "BangGia", action = "Details" }
             );

            #endregion Bảng giá

            #region HuongDan

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "HuongDan",
                url: "huong-dan",
                defaults: new { controller = "HuongDan", action = "Index" }
           );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
            name: "HuongDandetail",
            url: "huong-dan/{title}-{id}",
            defaults: new { controller = "HuongDan", action = "Details" }
             );

            #endregion HuongDan

            #region UngDungs

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "ungdung",
                url: "ung-dung",
                defaults: new { controller = "UngDung", action = "Index" }
           );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
            name: "ungdungdetail",
            url: "ung-dung/{title}-{id}",
            defaults: new { controller = "UngDung", action = "Details" }
             );

            #endregion UngDungs

            #region TemNhan

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "temnhan",
                url: "tem-nhan",
                defaults: new { controller = "TemNhan", action = "Index" }
           );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
            name: "temnhandetail",
            url: "tem-nhan/{title}-{id}",
            defaults: new { controller = "TemNhan", action = "Details" }
             );

            #endregion TemNhan

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
            name: "tintuc",
            url: "tin-tuc",
            defaults: new { controller = "TinTuc", action = "Index" }
       );

            routes.MapRoute(
            name: "contact",
            url: "lien-he",
            defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
             name: "doitac",
             url: "doi-tac",
             defaults: new { controller = "Home", action = "DoiTac", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "thanhtich",
            url: "thanh-tich",
            defaults: new { controller = "Home", action = "ThanhTich", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "hinhanh",
            url: "hinh-anh",
            defaults: new { controller = "Home", action = "HinhAnh", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "TinTucDetail",
               url: "tin-tuc/{cate}/{title}-{id}",
               defaults: new { controller = "TinTuc", action = "Details", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "duansub",
              url: "du-an/{title}-{id}",
              defaults: new { controller = "DuAn", action = "DuAnSub", id = UrlParameter.Optional }
           );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "search",
            url: "{title}-{id}",
            defaults: new { controller = "News", action = "Details", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                 name: "tintucsub",
                 url: "tin-tuc/{title}-{id}",
                 defaults: new { controller = "TinTuc", action = "TinTucSub", id = UrlParameter.Optional }
              );

            routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
        );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            ControllerBuilder.Current.DefaultNamespaces.Add("BlueDataWeb.Controllers");
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            RegisterRoutes(RouteTable.Routes);
            //Remove All View Engine including Webform and Razor
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Insert(0, new CustomViewEngine());
            RegisterMapTypes();
        }

        protected void Application_Error()
        {
            //if (Context.IsCustomErrorEnabled)
            //    ShowCustomErrorPage(Server.GetLastError());
        }

        private void ShowCustomErrorPage(Exception exception)
        {
            var httpException = exception as HttpException ?? new HttpException(500, "Internal Server Error", exception);

            Response.Clear();
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Error");
            routeData.Values.Add("fromAppErrorEvent", true);

            switch (httpException.GetHttpCode())
            {
                case 403:
                    routeData.Values.Add("action", "Unknown");
                    break;

                case 404:
                    routeData.Values.Add("action", "NotFound");
                    break;

                case 500:
                    routeData.Values.Add("action", "ServerError500");
                    break;

                default:
                    routeData.Values.Add("action", "Index");
                    routeData.Values.Add("httpStatusCode", httpException.GetHttpCode());
                    break;
            }

            Server.ClearError();

            IController controller = new ErrorController();
            controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
        }

        public void RegisterMapTypes()
        {
            //Mapper.CreateMap<NewsModels, News>().ForMember(dest => dest.BirthDay, conf => conf.MapFrom(src => Helper.Helper.ConvertToString(src.BirthDay)));
            Mapper.CreateMap<ArchitectureNewsDto, ArchitectureNew>();
            Mapper.CreateMap<ArchitectureNew, ArchitectureNewsDto>();

            Mapper.CreateMap<NewsModels, News>();
            Mapper.CreateMap<News, NewsModels>();
        }
    }
}