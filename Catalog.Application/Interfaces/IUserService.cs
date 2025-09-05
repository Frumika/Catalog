using Catalog.Application.DTO;

namespace Catalog.Application.Interfaces;

public interface IUserService
{
    Task<UserResponse> RegisterAsync(RegisterRequest request);
    Task<UserResponse> LoginAsync(LoginRequest request);
}