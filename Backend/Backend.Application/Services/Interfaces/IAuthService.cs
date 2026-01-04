using Backend.Application.DTO.Requests.Auth;
using Backend.Application.DTO.Responses;


namespace Backend.Application.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
    Task<AuthResponse> LogoutSessionAsync(LogoutSessionRequest request);
    Task<AuthResponse> LogoutAllSessionsAsync(int id);
}