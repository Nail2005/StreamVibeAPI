using AutoMapper;
using StreamVibeAPI.Business.Abstract;
using StreamVibeAPI.DAL.Abstract;
using StreamVibeAPI.DTOs;
using StreamVibeAPI.Entities;

namespace StreamVibeAPI.Business.Concrete
{
    public class FaqManager : IFaqService
    {
        private readonly IFaqRepository _faqRepository;
        private readonly IMapper _mapper;

        public FaqManager(IFaqRepository faqRepository, IMapper mapper)
        {
            _faqRepository = faqRepository;
            _mapper = mapper;
        }

        public async Task<List<FaqDto>> TGetAllAsync()
        {
            var faqs = await _faqRepository.GetAllAsync();
           return _mapper.Map<List<FaqDto>>(faqs);
        }
    }
}
