using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTiptop.Web.MallAdmin
{
    public class AreaRegistration : System.Web.Mvc.AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "malladmin";
            }
        }

        public override void RegisterArea(System.Web.Mvc.AreaRegistrationContext context)
        {
            //此路由不能删除
            context.MapRoute("malladmin2_default",
                              "malladmin/{controller}/{action}",
                              //new { controller = "home", action = "index", area = "malladmin" },
                              new { controller = "home", action = "default", area = "malladmin" },
                              new[] { "MyTiptop.Web.MallAdmin.Controllers" });

        }
    }
}
