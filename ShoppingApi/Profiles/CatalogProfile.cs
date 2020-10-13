using AutoMapper;
using ShoppingApi.Domain;
using ShoppingApi.Models.Catalog;
using ShoppingApi.Models.Curbside;

namespace ShoppingApi.Profiles
{
    public class CatalogProfile : Profile
    {
        
        public CatalogProfile(ConfigurationForMapper config)
        {
            // ShoppingItem -> GetCtalogResponseSummaryItem
            CreateMap<ShoppingItem, GetCatalogResponseSummaryItem>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src =>
                src.Cost * config.markUp)); // config.markUp

            CreateMap<PostCatalogRequest, ShoppingItem>()
                .ForMember(dest => dest.InInventory, opt => opt.MapFrom(src => true));

            CreateMap<PostCurbsideOrderRequest, CurbsideOrder>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => CurbsideOrderStatus.Pending));
        }
    }
}
