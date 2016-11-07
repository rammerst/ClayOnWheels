using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClayOnWheels.Startup))]
namespace ClayOnWheels
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
