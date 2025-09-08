using static Catalog.Application.Enums.IdentityResultCode;
using Catalog.Application.DTO;
using Catalog.Application.Interfaces;
using Catalog.Application.Logic;
using Catalog.DataAccess.Contexts;
using Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Services;

public class UserService : IUserService
{
    private readonly UsersDbContext _dbContext;

    public UserService(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<UserResponse> LoginAsync(LoginRequest request)
    {
        var validationResult = ValidateLoginRequest(request);
        if (validationResult is not null) return validationResult;

        try
        {
            var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Login == request.Login);

            if (user is null)
                return UserResponse.Fail(UserNotFound, "User not found");

            if (!Argon2Hasher.VerifyPassword(request.Password, user.HashPassword))
                return UserResponse.Fail(InvalidPassword, "Password is incorrect");
        }
        catch (Exception e)
        {
            return UserResponse.Fail(UnknownError, e.Message);
        }

        return UserResponse.Success("User logged in");
    }

    public async Task<UserResponse> RegisterAsync(RegisterRequest request)
    {
        var validationResult = ValidateRegisterRequest(request);
        if (validationResult is not null) return validationResult;
        
        try
        {
            bool isUserExist = await _dbContext.Users
                .AsNoTracking()
                .AnyAsync(user => user.Login == request.Login || user.Email == request.Email);

            if (isUserExist) return UserResponse.Fail(UserAlreadyExists, "User already exists");

            User user = new()
            {
                Email = request.Email,
                Login = request.Login,
                HashPassword = Argon2Hasher.HashPassword(request.Password)
            };
            _dbContext.Add(user);

            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return UserResponse.Fail(UnknownError, e.Message);
        }

        return UserResponse.Success("User registered");
    }

    private UserResponse? ValidateLoginRequest(LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Login))
            return UserResponse.Fail(InvalidLogin, "Empty login");

        if (string.IsNullOrWhiteSpace(request.Password))
            return UserResponse.Fail(InvalidPassword, "Empty password");

        return null;
    }

    private UserResponse? ValidateRegisterRequest(RegisterRequest request)
    {
        if (!UserValidator.IsValidEmail(request.Email))
            return UserResponse.Fail(InvalidEmail, "Incorrect email");

        if (!UserValidator.IsValidLogin(request.Login))
            return UserResponse.Fail(InvalidLogin, "Incorrect login");

        if (!UserValidator.IsValidPassword(request.Password))
            return UserResponse.Fail(InvalidPassword, "Incorrect password");

        return null;
    }
}