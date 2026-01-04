using Backend.Application.DTO.Responses;
using Backend.Application.StatusCodes;
using Backend.DataAccess.Storages.Interfaces;


namespace Backend.Application.Services;

public class CartService
{
    private readonly IUserSessionStorage _userStorage;
    private readonly ICartStateStorage _cartStorage;

    public CartService(ICartStateStorage cartStorage, IUserSessionStorage userStorage)
    {
        _cartStorage = cartStorage;
        _userStorage = userStorage;
    }

    public async Task<CartResponse> HandleProductAsync()
    {
        
        
        return CartResponse.Fail(CartStatusCode.UnknownError);
    }

    public async Task<CartResponse> RemoveProductAsync()
    {
        return CartResponse.Fail(CartStatusCode.UnknownError);
    }
}