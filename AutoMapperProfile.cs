using AutoMapper;
using Menu.Dtos;
using Menu.Dtos.Category;
using Menu.Dtos.Drink;
using Menu.Dtos.Section;
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

            CreateMap<Section, SectionDto>().ReverseMap();

            CreateMap<DrinkDto, Drink>().ReverseMap();

            CreateMap<Drink, DrinkDto>().ReverseMap();

            CreateMap<SectionCreateDto, Section>();

            CreateMap<Models.Item, ItemReadDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<Category, CategoryReadDto>();
            CreateMap<ItemUpdateDto, Models.Item>();
        }
    }
}