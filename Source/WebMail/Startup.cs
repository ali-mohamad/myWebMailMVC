using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(myWebMail.Startup))]
namespace myWebMail
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
