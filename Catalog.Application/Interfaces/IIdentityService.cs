using Catalog.Application.DTO.Requests;
using Catalog.Application.DTO.Responses;

namespace Catalog.Application.Interfaces;

public interface IIdentityService
{
    Task<IdentityResponse> RegisterAsync(RegisterRequest request);
    Task<IdentityResponse> LoginAsync(LoginRequest request);
}