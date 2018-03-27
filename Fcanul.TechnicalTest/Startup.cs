using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Fcanul.TechnicalTest.Startup))]
namespace Fcanul.TechnicalTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
