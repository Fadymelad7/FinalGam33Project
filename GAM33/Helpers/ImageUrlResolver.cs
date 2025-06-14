using AutoMapper;
using AutoMapper.Execution;
using GAM33.Dtos;
using Gma33.Core.Entites.StoreEntites;

namespace GAM33.Helpers
{
    public class ImageUrlResolver : IValueResolver<Product, ProductDto, string>

    {
        private readonly IConfiguration _configuration;

        public ImageUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImageUrl))
            {
                return $"{_configuration["BaseApiUrl"]}/{source.ImageUrl}";
            }

            return string.Empty;
        }
    }
}
