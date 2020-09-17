using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GameStoreUI.Startup))]
namespace GameStoreUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
