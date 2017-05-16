using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AkhilaKandi_FinalProject.Startup))]
namespace AkhilaKandi_FinalProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
