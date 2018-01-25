using Microsoft.Owin;
using Owin;
using StockExchange.Web.AutoMapper;

[assembly: OwinStartupAttribute(typeof(StockExchange.Web.Startup))]
namespace StockExchange.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            AutoMapperConfig.ConfigureAutoMapper();
        }
    }
}
