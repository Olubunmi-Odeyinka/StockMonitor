using AutoMapper;
using StockExchange.Domain;
using StockExchange.Domain.ViewModels;

namespace StockExchange.Web.AutoMapper
{
    public class AutoMapperProfileConfiguration : Profile
    {
        protected override void Configure()
        {
            CreateMap<UserStock, UserStockViewModel>()
           .ForMember(dst => dst.CompanyName, src => src.MapFrom<string>(e => e.Company.CompanyName))
           .ForMember(dst => dst.TicketSymbol, src => src.MapFrom<string>(e => e.Company.TicketSymbol))
           .ForMember(src => src.IsAdded, opt => opt.Ignore());

        //     public int Id { get; set; }
        //public string CompanyName { get; set; }
        //public string TicketSymbol { get; set; }
            CreateMap<Company, UserStockViewModel>()
            .ForMember(dst => dst.CompanyId, src => src.MapFrom<int>(e => e.Id))
            .ForMember(src => src.UserName, opt => opt.Ignore())
            .ForMember(src => src.IsAdded, opt => opt.Ignore());

        }
    }
}
