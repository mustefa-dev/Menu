using AutoMapper;
using Menu.Dtos;
using Menu.Dtos.Category;
using Menu.Dtos.Drink;
using Menu.Dtos.Drink.Menu.Dtos.Drink;
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

            CreateMap<Section, SectionDto>().ReverseMap();

            CreateMap<DrinkDto, Drink>()
                .ForMember(dest => dest.Photo, opt => opt.Ignore())
                .ForMember(dest => dest.Section, opt => opt.Ignore());

            CreateMap<DrinkCreateDto, Drink>()
                .ForMember(dest => dest.Photo, opt => opt.Ignore())
                .ForMember(dest => dest.Section, opt => opt.Ignore());

            CreateMap<Drink, DrinkDto>()
                .ForMember(dest => dest.SectionName, opt => opt.MapFrom(src => src.Section.Name));

            CreateMap<SectionCreateDto, Section>();

            CreateMap<ItemUpdateDto, Models.Item>();

            CreateMap<Models.Item, ItemReadDto>();

            CreateMap<CategoryCreateDto, Category>();

            CreateMap<CategoryUpdateDto, Category>();

            CreateMap<Category, CategoryReadDto>();
        }
    }
}