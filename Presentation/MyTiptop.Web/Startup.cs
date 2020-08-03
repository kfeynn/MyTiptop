using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyTiptop.Web.Startup))]
namespace MyTiptop.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
