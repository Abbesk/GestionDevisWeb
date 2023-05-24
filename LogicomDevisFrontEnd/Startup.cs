using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartup(typeof(LogicomDevisFrontEnd.Startup))]
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
