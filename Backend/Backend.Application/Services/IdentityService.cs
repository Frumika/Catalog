using Backend.Application.DTO.Requests.User;
using Backend.Application.DTO.Responses;
using Backend.Application.Logic;
using Backend.Application.Services.Interfaces;
using Backend.Application.StatusCodes;
using Backend.DataAccess.Contexts;
using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Backend.Application.Services;

public class IdentityService : IIdentityService
{
    private readonly MainDbContext _dbContext;

    public IdentityService(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserResponse> LoginAsync(LoginRequest request)
    {
        var validationResult = request.Validate();
        if (!validationResult.IsValid)
            return UserResponse.Fail(UserStatusCode.IncorrectData, validationResult.Message);

        try
        {
            var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Login == request.Login);

            if (user is null)
                return UserResponse.Fail(UserStatusCode.UserNotFound, "User not found");

            if (!Argon2Hasher.VerifyPassword(request.Password, user.HashPassword))
                return UserResponse.Fail(UserStatusCode.InvalidPassword, "Password is incorrect");
        }
        catch (Exception)
        {
            return UserResponse.Fail(UserStatusCode.UnknownError, "Internal server error");
        }

        return UserResponse.Success("User logged in");
    }

    public async Task<UserResponse> RegisterAsync(RegisterRequest request)
    {
        var validationResult = request.Validate();
        if (!validationResult.IsValid) 
            return UserResponse.Fail(UserStatusCode.IncorrectData, validationResult.Message);

        try
        {
            bool isUserExist = await _dbContext.Users
                .AsNoTracking()
                .AnyAsync(user => user.Login == request.Login);

            if (isUserExist) return UserResponse.Fail(UserStatusCode.UserAlreadyExists, "User already exists");

            User user = new()
            {
                Login = request.Login,
                HashPassword = Argon2Hasher.HashPassword(request.Password)
            };
            _dbContext.Add(user);

            await _dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            return UserResponse.Fail(UserStatusCode.UnknownError, "Internal server error");
        }

        return UserResponse.Success("User registered");
    }
}