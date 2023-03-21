using AutoMapper;
using Domain.Entitites.Categories;
using DTO.Categories;

namespace API.Mappings.Categories
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            #region Create

            CreateMap<CategoryAddRequestDto, Category>()
                .ReverseMap();

            #endregion Create

            #region Read

            CreateMap<Category, CategoryResponseDto>()
                .ReverseMap()
                .ForAllMembers(x => x.Condition(
                    (src, dest, property) =>
                    {
                        // Let's ignore both null and empty string properties on category
                        if (property == null) return false;
                        if (property.GetType() == typeof(string) && string.IsNullOrEmpty((string)property)) return false;

                        return true;
                    }));

            #endregion Read

            #region Update

            CreateMap<CategoryUpdateRequestDto, Category>().ReverseMap();

            #endregion Update

        }
    }
}
