using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyTiptop.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.asmx/{*pathInfo}"); //webservice 服务不经过路由
            routes.IgnoreRoute("{*allasmx}", new { allasmx = @".*\.asmx(/.*)?" });

            //默认路由(此路由不能删除)
            routes.MapRoute("default",
                            "{controller}/{action}",
                            //new { controller = "home", action = "index" },
                             new { controller = "Account", action = "login" },   //默认到登陆页。不再指向前台
                            new[] { "MyTiptop.Web.Controllers" });

            //routes.MapRoute(
            //               "Default",
            //               "{controller}/{action}/{id}",
            //               new { controller = "home", action = "Index", id = UrlParameter.Optional },
            //               new string[] { "MyTiptop.Web.Controllers" }
            //               );


        }
    }
}
