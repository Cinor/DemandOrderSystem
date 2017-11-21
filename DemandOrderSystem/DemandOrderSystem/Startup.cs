using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DemandOrderSystem.Startup))]
namespace DemandOrderSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
