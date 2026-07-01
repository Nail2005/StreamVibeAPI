using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StreamVibeAPI.Business.Abstract;
using StreamVibeAPI.DAL.Abstract;
using StreamVibeAPI.DTOs.Auth;
using StreamVibeAPI.Entities;
using StreamVibeAPI.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace StreamVibeAPI.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ITokenService _tokenManager;
        public UserManager(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, ITokenService tokenManager)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _tokenManager = tokenManager;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || request.Username.Length < 3 || request.Username.Length > 20)
            {
                throw new Exception("Username must be between 3 and 20 characters.");
            }

            if (string.IsNullOrWhiteSpace(request.Email) || !new EmailAddressAttribute().IsValid(request.Email))
            {
                throw new Exception("Invalid email format.");
            }

            if (string.IsNullOrWhiteSpace(request.Password) || request.Password.Length < 8)
            {
                throw new BadRequestException("Password must be at least 8 characters");
            }

            var existingEmail = await _userRepository.GetByEmailAsync(request.Email);
            if (existingEmail != null)
            {
                throw new ConflictException("Email already in use.");
            }

            var existingUsername = await _userRepository.GetByUsernameAsync(request.Username);
            if (existingUsername != null)
            {
                throw new ConflictException("Username already taken.");
            }

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password, 10),
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            var accessToken = _tokenManager.GenerateAccessToken(user);
            var refreshTokenValue = _tokenManager.GenerateRefreshToken();

            var refreshToken = new RefreshToken
            {
                UserId = user.Id,
                Token = refreshTokenValue,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow
            };

            await _refreshTokenRepository.AddAsync(refreshToken);
            await _refreshTokenRepository.SaveChangesAsync();

            var result = new AuthResponseDto
            {
                User = new UserResponseDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt
                },
                AccessToken = accessToken,
                RefreshToken = refreshTokenValue,
                ExpiresIn = 900
            };

            return result;

        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new BadRequestException("Email is required.");
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                throw new BadRequestException("Password is required.");
            }

            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                throw new UnauthorizedException("Invalid email or password.");
            }

            var passwordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!passwordValid)
            {
                throw new UnauthorizedException("Invalid email or password.");
            }

            if (!user.IsActive)
            {
                throw new ForbiddenException("User account is deactivated.");
            }

            await _refreshTokenRepository.DeleteUserTokensAsync(user.Id);
            await _refreshTokenRepository.SaveChangesAsync();

            var accessToken = _tokenManager.GenerateAccessToken(user);

            var refreshTokenValue = _tokenManager.GenerateRefreshToken();

            var refreshToken = new RefreshToken
            {
                UserId = user.Id,
                Token = refreshTokenValue,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow
            };

            await _refreshTokenRepository.AddAsync(refreshToken);
            await _refreshTokenRepository.SaveChangesAsync();

            var result = new AuthResponseDto
            {
                User = new UserResponseDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt
                },
                AccessToken = accessToken,
                RefreshToken = refreshTokenValue,
                ExpiresIn = 900
            };

            return result;
        }

        public async Task<RefreshTokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.RefreshToken))
            {
                throw new BadRequestException("Refresh token is required.");
            }

            var existingToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken);
            if (existingToken == null)
            {
                throw new UnauthorizedException("Invalid refresh token.");
            }

            if (existingToken.ExpiresAt < DateTime.UtcNow)
            {
                await _refreshTokenRepository.DeleteAsync(existingToken);
                await _refreshTokenRepository.SaveChangesAsync();
                throw new UnauthorizedException("Refresh token has expired.");
            }

            var user = await _userRepository.GetByIdAsync(existingToken.UserId);
            if (user == null)
            {
                throw new UnauthorizedException("User not found.");
            }

            await _refreshTokenRepository.DeleteAsync(existingToken);

            var newRefreshTokenValue = _tokenManager.GenerateRefreshToken();

            var newRefreshToken = new RefreshToken
            {
                UserId = user.Id,
                Token = newRefreshTokenValue,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };

            await _refreshTokenRepository.AddAsync(newRefreshToken);
            await _refreshTokenRepository.SaveChangesAsync();

            var result = new RefreshTokenResponseDto
            {
                AccessToken = _tokenManager.GenerateAccessToken(user),
                RefreshToken = newRefreshTokenValue,
                ExpiresIn = 900
            };

            return result;
        }

        public async Task LogoutAsync(LogoutRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.RefreshToken))
            {
                return;
            }

            var token = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken);

            if (token != null)
            {
                await _refreshTokenRepository.DeleteAsync(token);
                await _refreshTokenRepository.SaveChangesAsync();
            }

        }

        public async Task<UserResponseDto> MeAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            var result = new UserResponseDto
            {
                Id = userId,
                Username = user.Username,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
            };

            return result;
        }

        public async Task<UserProfileSubscribeDto> GetProfileAsync(int userId)
        {
            var user = await _userRepository.GetProfileAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("User not found");
            }


            var subscription =
                         user.UserSubscription != null &&
                         (user.UserSubscription.Status == "active" ||
                         user.UserSubscription.Status == "trial")
                         ? user.UserSubscription
                         : null;

            var result = new UserProfileSubscribeDto
            { 
                User = new UserResponseDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt,
                },

                Subscription = subscription == null ? null
                                 : new UserSubscriptionProfileDto
                                 {
                                     Id = subscription.Id,
                                     BillingCycle = subscription.BillingCycle,
                                     IsTrial = subscription.IsTrial,
                                     Status = subscription.Status,
                                     StartedAt = subscription.StartedAt,
                                     ExpiresAt = subscription.ExpiresAt,

                                     Plan = new UserSubscriptionPlanDto
                                     {
                                         Id = subscription.Plan.Id,
                                         Name = subscription.Plan.Name,
                                         Price = subscription.BillingCycle == "monthly" ? subscription.Plan.PriceMonthly : subscription.Plan.PriceYearly
                                     }
                                 }
            };
            return result;
        }
    }
}
