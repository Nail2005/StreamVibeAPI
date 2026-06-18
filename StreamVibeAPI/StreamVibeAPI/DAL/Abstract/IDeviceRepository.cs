using StreamVibeAPI.Entities;

namespace StreamVibeAPI.DAL.Abstract
{
    public interface IDeviceRepository
    {
        Task<List<Device>> GetAllAsync();
    }
}
