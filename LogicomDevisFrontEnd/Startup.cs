
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LogicomDevisFrontEnd.Startup))]
namespace LogicomDevisFrontEnd
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
