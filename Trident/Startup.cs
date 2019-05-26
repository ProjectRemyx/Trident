using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Trident.Startup))]
namespace Trident
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
