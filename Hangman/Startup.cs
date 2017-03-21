using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Hangman.Startup))]
namespace Hangman
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
