using AutoMapper;
using Menu.Dtos;
using Menu.Dtos.Category;
using Menu.Models;

namespace Menu
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ItemCreateDto, Models.Item>()
                .ForMember(dest => dest.Photo, opt => opt.Ignore());

            CreateMap<ItemCreateDto, Models.Item>()
                .ForMember(dest => dest.Photo, opt => opt.Ignore());
            
            CreateMap<Models.Item, ItemReadDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<Category, CategoryReadDto>();
            CreateMap<ItemUpdateDto, Models.Item>(); 

        }
    }
}