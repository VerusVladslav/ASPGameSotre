using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GameStoreCoursework.Startup))]
namespace GameStoreCoursework
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
