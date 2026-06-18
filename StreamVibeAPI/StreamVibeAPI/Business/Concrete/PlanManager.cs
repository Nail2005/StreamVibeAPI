using AutoMapper;
using StreamVibeAPI.Business.Abstract;
using StreamVibeAPI.DAL.Abstract;
using StreamVibeAPI.DTOs;
using StreamVibeAPI.Entities;

namespace StreamVibeAPI.Business.Concrete
{
    public class PlanManager : IPlanService
    {
        private readonly IPlanRepository _planRepository;
        private readonly IMapper _mapper;
        public PlanManager(IPlanRepository planRepository, IMapper mapper)
        {
            _planRepository = planRepository;
            _mapper = mapper;
        }

        public async Task<List<PlanDto>> TGetAllAsync(string billing)
        {
            billing = (billing ?? "monthly").ToLower();

            var plans = await _planRepository.GetAllAsync();

            return plans.Select(x => new PlanDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                IsPopular = x.IsPopular ?? false,

                Price = billing == "yearly"
                    ? x.PriceYearly
                    : x.PriceMonthly
            }).ToList();
        }
    }
}
