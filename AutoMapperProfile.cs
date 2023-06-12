using AutoMapper;
using Menu.Dtos.Category;
using Menu.Dtos.Drink;
using Menu.Dtos.Drink.Menu.Dtos.Drink;
using Menu.Dtos.Food;
using Menu.Dtos.FoodSection;
using Menu.Dtos.Section;
using Menu.Models;

namespace Menu
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<FoodSectionReadDto, FoodSection>().ReverseMap();
            CreateMap<FoodReadDto, Food>().ReverseMap();
            CreateMap<DrinkCreateDto, Drink>().ReverseMap();
            CreateMap<SectionReadDto, Section>().ReverseMap();
            CreateMap<Food, FoodCreateDto>().ReverseMap();
            CreateMap<Drink,DrinkReadDto>().ReverseMap();
            CreateMap<Section, SectionCreateDto>().ReverseMap();
            CreateMap<FoodSection, FoodSectionCreateDto>().ReverseMap();



        }
    }
}