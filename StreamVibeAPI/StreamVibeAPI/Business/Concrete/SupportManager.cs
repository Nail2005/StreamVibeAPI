using Azure.Core;
using StreamVibeAPI.Business.Abstract;
using StreamVibeAPI.DAL.Abstract;
using StreamVibeAPI.DTOs;
using StreamVibeAPI.Entities;
using StreamVibeAPI.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace StreamVibeAPI.Business.Concrete
{
    public class SupportManager : ISupportService
    {
        private readonly IContactMessageRepository _repository;

        public SupportManager(IContactMessageRepository repository)
        {
            _repository = repository;
        }

        public async Task<ContactMessageResponseDto> SendMessageAsync(ContactMessageRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FirstName))
            {
                throw new BadRequestException("First name is required");
            }

            if (string.IsNullOrWhiteSpace(dto.LastName))
            {
                throw new BadRequestException("Last name is required");
            }

            if (!new EmailAddressAttribute().IsValid(dto.Email))
            {
                throw new BadRequestException("Invalid email format");
            }

            if (string.IsNullOrWhiteSpace(dto.PhoneCountryCode))
            {
                throw new BadRequestException("Phone country code is required");
            }

            if (string.IsNullOrWhiteSpace(dto.PhoneNumber))
            {
                throw new BadRequestException("Phone number is required");
            }

            if (!dto.PhoneNumber.All(char.IsDigit))
            {
                throw new BadRequestException("Phone number must contain only digits");
            }

            if (string.IsNullOrWhiteSpace(dto.Message))
            {
                throw new BadRequestException("Message is required");
            }

            if (dto.Message.Length < 20)
            {
                throw new BadRequestException("Message must be at least 20 characters");
            }

            var entity = new ContactMessage
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,  
                PhoneCountryCode = dto.PhoneCountryCode,
                PhoneNumber = dto.PhoneNumber,
                Message = dto.Message,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(entity);   
            await _repository.SaveChangesAsync();

            var responseData = new ContactMessageResponseDto
            {
                Id = entity.Id,
                CreatedAt = entity.CreatedAt,   
            };

            return responseData;    

        }
    }
}
