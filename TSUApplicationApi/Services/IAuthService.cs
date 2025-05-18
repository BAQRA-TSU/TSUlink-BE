using TSUApplicationApi.Entities;
using TSUApplicationApi.Models;

namespace TSUApplicationApi.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(RegisterUserDto request);
        Task<TokenResponseDto?> LoginAsync(LoginUserDto request);
        Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto request);
    }
}
