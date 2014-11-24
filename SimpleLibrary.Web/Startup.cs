using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimpleLibrary.Web.Startup))]
namespace SimpleLibrary.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
