using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ApartmanYonetimi.Startup))]
namespace ApartmanYonetimi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
