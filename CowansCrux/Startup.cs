using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CowansCrux.Startup))]
namespace CowansCrux
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
