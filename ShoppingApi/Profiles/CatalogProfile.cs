using AutoMapper;
using ShoppingApi.Domain;
using ShoppingApi.Models.Catalog;

namespace ShoppingApi.Profiles
{
    public class CatalogProfile : Profile
    {
        
        public CatalogProfile(ConfigurationForMapper config)
        {
            // ShoppingItem -> GetCtalogResponseSummaryItem
            CreateMap<ShoppingItem, GetCatalogResponseSummaryItem>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src =>
                src.Cost * 1.5M)); // config.markUp

            CreateMap<PostCatalogRequest, ShoppingItem>()
                .ForMember(dest => dest.InInventory, opt => opt.MapFrom(src => true));
        }
    }
}
