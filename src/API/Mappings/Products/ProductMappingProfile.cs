using AutoMapper;
using Domain.Entitites.Products;
using DTO.Products;

namespace API.Mappings.Products
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            #region Create

            CreateMap<ProductCreateRequestDto, Product>().ReverseMap();

            #endregion Create

            #region Read

            CreateMap<Product, ProductResponseDto>().ReverseMap()
                .ForAllMembers(x => x.Condition(
                    (src, dest, property) =>
                    {
                        // Let's ignore both null and empty string properties on product
                        if (property == null) return false;
                        if (property.GetType() == typeof(string) && string.IsNullOrEmpty((string)property)) return false;

                        return true;
                    }));

            #endregion Read

            #region Update

            CreateMap<ProductUpdateRequestDto, Product>().ReverseMap();

            #endregion Update
        }


    }
}
