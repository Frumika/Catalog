using Backend.Application.DTO.Requests.User;
using Backend.Application.DTO.Responses;


namespace Backend.Application.Services.Interfaces;

public interface IUserService
{
    Task<UserResponse> RegisterAsync(RegisterRequest request);
    Task<UserResponse> LoginAsync(LoginRequest request);
}