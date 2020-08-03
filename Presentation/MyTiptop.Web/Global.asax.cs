using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using MyTiptop.Core;
using MyTiptop.Web.Framework;

namespace MyTiptop.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //禁止EF自动生成数据库
            System.Data.Entity.Database.SetInitializer<MyTiptop.Core.DBContext>(null);
            System.Data.Entity.Database.SetInitializer<MyTiptop.OraCore.OraDBContext>(null);
            System.Data.Entity.Database.SetInitializer<MyTiptop.SupplierData.DBContext>(null);

            //将默认视图引擎替换为ThemeRazorViewEngine引擎.用于分Area 分项目后找目标VIEW
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ThemeRazorViewEngine());

            AreaRegistration.RegisterAllAreas();
           // FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);


        }
    }
}
