using Backend.Application.DTO.Requests;
using Backend.Application.DTO.Requests.User;
using Backend.Application.DTO.Responses;


namespace Backend.Application.Services.Interfaces;

public interface IIdentityService
{
    Task<UserResponse> RegisterAsync(RegisterRequest request);
    Task<UserResponse> LoginAsync(LoginRequest request);
}