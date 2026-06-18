using AutoMapper;
using StreamVibeAPI.Business.Abstract;
using StreamVibeAPI.DAL.Abstract;
using StreamVibeAPI.DTOs;
using StreamVibeAPI.Entities;

namespace StreamVibeAPI.Business.Concrete
{
    public class DeviceManager : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IMapper _mapper;

        public DeviceManager(IDeviceRepository deviceRepository, IMapper mapper)
        {
            _deviceRepository = deviceRepository;
            _mapper = mapper;
        }

        public async Task<List<DeviceDto>> TGetAllAsync()
        {
            var devices = await _deviceRepository.GetAllAsync();

            return _mapper.Map<List<DeviceDto>>(devices);
         
        }
    }
}
