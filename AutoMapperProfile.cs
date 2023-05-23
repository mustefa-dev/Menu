using AutoMapper;
using Menu.Dtos;
using Menu.Dtos.Menu.Dtos;
using Menu.Models;

namespace Menu
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
                .ForMember(dest => dest.Water, opt => opt.MapFrom(src => src.Water))
                .ReverseMap();

            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Water, WaterDto>().ReverseMap();
            CreateMap<Item, ItemDto>().ReverseMap();
            // Add mappings for other classes if needed
        }
    }
}