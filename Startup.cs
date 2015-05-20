using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NoteTakerApplication.Startup))]
namespace NoteTakerApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
