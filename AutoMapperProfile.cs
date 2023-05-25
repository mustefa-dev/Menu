using Auth.Dtos.Item;
using AutoMapper;
using Menu.Models;

namespace Menu
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ItemCreateDto, Models.Item>()
                .ForMember(dest => dest.Photo, opt => opt.Ignore());

            CreateMap<ItemUpdateDto, Models.Item>()
                .ForMember(dest => dest.Photo, opt => opt.Ignore());
            
            CreateMap<Models.Item, ItemReadDto>();
        }
    }
}