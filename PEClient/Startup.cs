using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PEClient.Startup))]
namespace PEClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
