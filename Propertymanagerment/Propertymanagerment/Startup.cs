using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Propertymanagerment.Startup))]
namespace Propertymanagerment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
