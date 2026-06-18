using StreamVibeAPI.DTOs;
using StreamVibeAPI.Entities;

namespace StreamVibeAPI.Business.Abstract
{
    public interface IGenreService
    {
        Task<List<GenreDto>> TGetAllAsync(string type);   
    }
}
