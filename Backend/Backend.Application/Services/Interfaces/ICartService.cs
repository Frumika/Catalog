using Backend.Application.DTO.Requests.Cart;
using Backend.Application.DTO.Responses;


namespace Backend.Application.Services.Interfaces;

public interface ICartService
{
    Task<CartResponse> HandleProductAsync(HandleProductRequest request);
    Task<CartResponse> RemoveProductAsync(RemoveProductRequest request);
}