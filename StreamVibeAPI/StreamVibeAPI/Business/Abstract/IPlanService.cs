using StreamVibeAPI.DTOs;
using StreamVibeAPI.Entities;

namespace StreamVibeAPI.Business.Abstract
{
    public interface IPlanService
    {
        Task<List<PlanDto>> TGetAllAsync(string billing);
    }
}
