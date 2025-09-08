using Catalog.Application.DTO;

namespace Catalog.Application.Interfaces;

public interface IIdentityService
{
    Task<IdentityResponse> RegisterAsync(RegisterRequest request);
    Task<IdentityResponse> LoginAsync(LoginRequest request);
}