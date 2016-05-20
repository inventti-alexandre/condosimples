using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CondoSimples.Startup))]
namespace CondoSimples
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
