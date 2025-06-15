using AutoMapper;
using GAM33.Dtos;
using Gma33.Core.Entites.IdentityEntites;
using Gma33.Core.Entites.OrderModule;
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
            CreateMap<Cart, CartDto>().ReverseMap();

            CreateMap<CartProduct, CartProductDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.Product.ImageUrl))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Product.Details));

            CreateMap<AddressDto, OrderAddress>();





        }
    }
}
