using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bdf.Sample.Web.Mvc.Startup))]
namespace Bdf.Sample.Web.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
