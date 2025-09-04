using Catalog.Application.DTO;

namespace Catalog.Application.Interfaces;

public interface IUserService
{
    Task<UserDbResponse> RegisterAsync(RegisterRequest request);
    Task<UserDbResponse> LoginAsync(LoginRequest request);
}