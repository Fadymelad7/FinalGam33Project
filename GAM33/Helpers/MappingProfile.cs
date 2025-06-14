using AutoMapper;
using GAM33.Dtos;
using Gma33.Core.Entites.IdentityEntites;
using Gma33.Core.Entites.StoreEntites;

namespace GAM33.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                                              .ForMember(d => d.ImageUrl, o => o.MapFrom<ImageUrlResolver>());

            CreateMap<Category, CategoryDto>();
            CreateMap<Address, AddressDto>().ReverseMap();




        }
    }
}
