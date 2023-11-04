using AutoMapper;
using Talabat.Core.Entities;
using Talabat.Dtos;

namespace Talabat.Helpers
{
    public class PictureUrlResolver : IValueResolver<Product, ProductToReturnDtos, string>
    {
        public PictureUrlResolver(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public string Resolve(Product source, ProductToReturnDtos destination, string destMember, ResolutionContext context)
        {
           if(!string.IsNullOrEmpty(source.PictureUrl)) {
                return $"{Configuration["ApiBaseUrl"]}{source.PictureUrl}";

               
 
            }

            return string.Empty;
        }
    }
}
