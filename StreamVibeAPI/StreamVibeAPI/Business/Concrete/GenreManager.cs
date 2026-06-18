using AutoMapper;
using StreamVibeAPI.Business.Abstract;
using StreamVibeAPI.DAL.Abstract;
using StreamVibeAPI.DTOs;
using StreamVibeAPI.Entities;
using System.Runtime.CompilerServices;

namespace StreamVibeAPI.Business.Concrete
{
    public class GenreManager : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreManager(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<List<GenreDto>> TGetAllAsync(string type)
        {
            
            var genres = type == "all" || string.IsNullOrEmpty(type)
            ? await _genreRepository.GetAllAsync()
            : await _genreRepository.GetByTypeAsync(type);

            return _mapper.Map<List<GenreDto>>(genres);
        }
    }
}
