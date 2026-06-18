using StreamVibeAPI.DTOs;
using StreamVibeAPI.Entities;

namespace StreamVibeAPI.Business.Abstract
{
    public interface IDeviceService
    {
        Task<List<DeviceDto>> TGetAllAsync();
    }
}
