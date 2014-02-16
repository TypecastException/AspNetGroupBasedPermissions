using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AspNetGroupBasedPermissions.Startup))]
namespace AspNetGroupBasedPermissions
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
