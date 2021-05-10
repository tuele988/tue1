using System.Web.Mvc;

namespace shopaoquan.Areas.admin
{
    public class adminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AdminLogin",
                "Admin/Login",
                new { Controller = "Auth", action = "Login", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "admin_default",
                "admin/{controller}/{action}/{id}",
                new { Controller = "ShopAoQuan", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}