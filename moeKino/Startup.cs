using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(moeKino.Startup))]
namespace moeKino
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
