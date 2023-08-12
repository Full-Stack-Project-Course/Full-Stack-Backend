using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProdcutToReturnDto, string>
    {
        private readonly IConfiguration _configuration;
        public ProductUrlResolver(IConfiguration configuration)
        {

            _configuration = configuration;

        }
        public string Resolve(Product source, ProdcutToReturnDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl)) {
                string baseUri = _configuration["ApiUri"];
                return baseUri + source.PictureUrl;
            }

            return null;
        }
    }
}
