
using AutoMapper;
using onine.core.Entites;
using onineproject.DTOs;

namespace onineproject.Helpers
{
    public class ProductpictureurlResolver : IValueResolver<Product, ProductToReturnDTOs, string>
    {
        private readonly IConfiguration  _configuration;
        public ProductpictureurlResolver( IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDTOs destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            { 
                return $"{_configuration["ApiBaseUrl"]}{source.PictureUrl}";
            }
            else
            {
                return string.Empty;
            }
            
        }
    }
}
