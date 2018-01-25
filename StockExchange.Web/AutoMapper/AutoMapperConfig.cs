using AutoMapper;

namespace StockExchange.Web.AutoMapper
{
   public class AutoMapperConfig
    {
        public static IMapper Mapper;
        public static void ConfigureAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfileConfiguration());
            });

            Mapper = config.CreateMapper();
            config.AssertConfigurationIsValid();
        }
    }
}
