using Backend.Application.DTO.Requests;
using Backend.Application.DTO.Responses;

namespace Backend.Application.Interfaces;

public interface IIdentityService
{
    Task<IdentityResponse> RegisterAsync(RegisterRequest request);
    Task<IdentityResponse> LoginAsync(LoginRequest request);
}