using AutoMapper;
using StreamVibeAPI.Business.Concrete;
using StreamVibeAPI.DTOs;
using StreamVibeAPI.Entities;

namespace StreamVibeAPI.Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Genre, GenreDto>().ReverseMap();
            CreateMap<Device, DeviceDto>().ReverseMap();
            CreateMap<PricingPlan, PlanDto>().ReverseMap();
            CreateMap<Faq, FaqDto>().ReverseMap();
            CreateMap<Content, ContentDto>().ReverseMap();
            CreateMap<Content, HeroDto>().ReverseMap(); 
            CreateMap<Review, ReviewDto>().ReverseMap();    

        }
    }
}
