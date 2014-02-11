using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AspNetExtendingIdentityRoles.Startup))]
namespace AspNetExtendingIdentityRoles
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
