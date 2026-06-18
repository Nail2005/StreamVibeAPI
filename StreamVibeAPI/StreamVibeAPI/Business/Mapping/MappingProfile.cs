using AutoMapper;
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
        }
    }
}
