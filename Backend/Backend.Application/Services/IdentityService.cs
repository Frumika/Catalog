using Backend.Application.DTO.Requests;
using Backend.Application.DTO.Responses;
using Backend.Application.Interfaces;
using Backend.Application.Logic;
using static Backend.Application.Enums.IdentityStatus;
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
    
    public async Task<IdentityResponse> LoginAsync(LoginRequest request)
    {
        var validationResult = ValidateLoginRequest(request);
        if (validationResult is not null) return validationResult;

        try
        {
            var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Login == request.Login);

            if (user is null)
                return IdentityResponse.Fail(UserNotFound, "User not found");

            if (!Argon2Hasher.VerifyPassword(request.Password, user.HashPassword))
                return IdentityResponse.Fail(InvalidPassword, "Password is incorrect");
        }
        catch (Exception)
        {
            return IdentityResponse.Fail(UnknownError, "Internal server error");
        }

        return IdentityResponse.Success("User logged in");
    }

    public async Task<IdentityResponse> RegisterAsync(RegisterRequest request)
    {
        var validationResult = ValidateRegisterRequest(request);
        if (validationResult is not null) return validationResult;
        
        try
        {
            bool isUserExist = await _dbContext.Users
                .AsNoTracking()
                .AnyAsync(user => user.Login == request.Login);

            if (isUserExist) return IdentityResponse.Fail(UserAlreadyExists, "User already exists");

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
            return IdentityResponse.Fail(UnknownError,"Internal server error");
        }

        return IdentityResponse.Success("User registered");
    }

    private IdentityResponse? ValidateLoginRequest(LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Login))
            return IdentityResponse.Fail(InvalidLogin, "Empty login");

        if (string.IsNullOrWhiteSpace(request.Password))
            return IdentityResponse.Fail(InvalidPassword, "Empty password");

        return null;
    }

    private IdentityResponse? ValidateRegisterRequest(RegisterRequest request)
    {
        if (!IdentityValidator.IsValidEmail(request.Email))
            return IdentityResponse.Fail(InvalidEmail, "Incorrect email");

        if (!IdentityValidator.IsValidLogin(request.Login))
            return IdentityResponse.Fail(InvalidLogin, "Incorrect login");

        if (!IdentityValidator.IsValidPassword(request.Password))
            return IdentityResponse.Fail(InvalidPassword, "Incorrect password");

        return null;
    }
}