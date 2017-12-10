using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LeavePortal.Web.Startup))]
namespace LeavePortal.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
