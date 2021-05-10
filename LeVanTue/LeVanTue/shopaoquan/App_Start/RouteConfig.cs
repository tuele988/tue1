using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace shopaoquan
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "LienHe",
               url: "lien-he",
               defaults: new { controller = "SendMail", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
             name: "gioithieu",
             url: "gioi-thieu",
             defaults: new { controller = "home", action = "post", id = UrlParameter.Optional }
         );
            routes.MapRoute(
            name: "GioHangThem",
            url: "gio-hang/them",
            defaults: new { controller = "giohang", action = "Index", id = UrlParameter.Optional }
        );
            routes.MapRoute(
             name: "Khachhang",
             url: "khach-hang",
             defaults: new { controller = "Khachhang", action = "Index", id = UrlParameter.Optional }
         );
            routes.MapRoute(
            name: "KhachhangDangnhap",
            url: "khach-hang/dang-nhap",
            defaults: new { controller = "Khachhang", action = "Index", id = UrlParameter.Optional }
        );
            routes.MapRoute(
            name: "KhachhangDangxuat",
            url: "khach-hang/dang-xuat",
            defaults: new { controller = "Khachhang", action = "Index", id = UrlParameter.Optional }
        );
            routes.MapRoute(
              name: "Timkiem",
              url: "tim-kiem",
              defaults: new { controller = "TimKiem", action = "Index", id = UrlParameter.Optional }
          );
            routes.MapRoute(
             name: "sanpham",
             url: "san-pham",
             defaults: new { controller = "Home", action = "Product", id = UrlParameter.Optional }
         );
            routes.MapRoute(
            name: "tintuc",
            url: "tin-tuc",
            defaults: new { controller = "Home", action = "post", id = UrlParameter.Optional }
        );



            routes.MapRoute(
               name: "SiteSLug",
               url: "{slug}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
