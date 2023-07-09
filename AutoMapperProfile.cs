using AutoMapper;
using Menu.Dtos;
using Menu.Dtos.Category;
using Menu.Dtos.Section;

namespace Menu
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryReadDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();

            CreateMap<Section, SectionReadDto>();
            CreateMap<SectionCreateDto, Section>();
            CreateMap<SectionUpdateDto, Section>();

            CreateMap<Item, ItemReadDto>();
            CreateMap<ItemCreateDto, Item>();
            CreateMap<ItemUpdateDto, Item>();
        }
    }
}