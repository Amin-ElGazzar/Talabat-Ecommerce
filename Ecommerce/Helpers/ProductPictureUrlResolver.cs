using AutoMapper;
using Ecommerce.Domain.Entities;
using Ecommerce.Dtos;

namespace Ecommerce.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToreturnDto, string>
    {
        private readonly IConfiguration configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(Product source, ProductToreturnDto destination, string destMember, ResolutionContext context)
        {

            if (!string.IsNullOrEmpty(source.PictureUrl))
                 return $"{configuration["ApiBaseUrl"]}{source.PictureUrl}";
            return string.Empty ;
        }
    }
}
