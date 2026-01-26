using Backend.Application.DTO.Requests.Auth;
using Backend.Application.DTO.Responses;
using Backend.Application.Logic;
using Backend.Application.StatusCodes;
using Backend.DataAccess.Postgres.Contexts;
using Backend.DataAccess.Storages;
using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using UserSessionDto = Backend.DataAccess.Storages.DTO.UserSessionDto;


namespace Backend.Application.Services;

public class AuthService
{
    private readonly MainDbContext _dbContext;
    private readonly UserSessionStorage _userSessionStorage;

    public AuthService(MainDbContext dbContext, UserSessionStorage userSessionStorage)
    {
        _dbContext = dbContext;
        _userSessionStorage = userSessionStorage;
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var validationResult = request.Validate();
        if (!validationResult.IsValid)
            return AuthResponse.Fail(AuthStatusCode.BadRequest, validationResult.Message);

        try
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Login == request.Login);

            if (user is null)
                return AuthResponse.Fail(AuthStatusCode.UserNotFound, "User not found");

            if (!Argon2Hasher.VerifyPassword(request.Password, user.HashPassword))
                return AuthResponse.Fail(AuthStatusCode.InvalidPassword, "Password is incorrect");

            string sessionId = Guid.NewGuid().ToString();
            UserSessionDto session = new(user);
            await _userSessionStorage.SetSessionAsync(sessionId, session);

            return AuthResponse.Success(new DTO.Entities.Auth.UserSessionDto
            {
                SessionId = sessionId,
                Login = session.Login
            }, "User has beel logged in");
        }
        catch (Exception)
        {
            return AuthResponse.Fail(AuthStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var validationResult = request.Validate();
        if (!validationResult.IsValid)
            return AuthResponse.Fail(AuthStatusCode.BadRequest, validationResult.Message);

        try
        {
            bool isUserExist = await _dbContext.Users
                .AsNoTracking()
                .AnyAsync(user => user.Login == request.Login);

            if (isUserExist) return AuthResponse.Fail(AuthStatusCode.UserAlreadyExists, "User already exists");

            User user = new()
            {
                Login = request.Login,
                HashPassword = Argon2Hasher.HashPassword(request.Password)
            };

            _dbContext.Add(user);
            await _dbContext.SaveChangesAsync();

            return AuthResponse.Success("User registered");
        }
        catch (Exception)
        {
            return AuthResponse.Fail(AuthStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<AuthResponse> LogoutSessionAsync(LogoutSessionRequest request)
    {
        var validationResult = request.Validate();
        if (!validationResult.IsValid)
            return AuthResponse.Fail(AuthStatusCode.BadRequest, validationResult.Message);

        try
        {
            bool isLogout = await _userSessionStorage.LogoutSessionAsync(request.SessionId);
            if (!isLogout) return AuthResponse.Fail(AuthStatusCode.SessionNotFound, "Incorrect session id");

            return isLogout
                ? AuthResponse.Success("The user has been logged out")
                : AuthResponse.Fail(AuthStatusCode.SessionNotFound, "Incorrect session id");
        }
        catch (Exception)
        {
            return AuthResponse.Fail(AuthStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<AuthResponse> LogoutAllSessionsAsync(int id)
    {
        if (id <= 0)
            return AuthResponse.Fail(AuthStatusCode.BadRequest, "Id must be greater than 0");

        try
        {
            bool isLogout = await _userSessionStorage.LogoutAllSessionsAsync(id);

            return isLogout
                ? AuthResponse.Success("The user has been logged out of all sessions")
                : AuthResponse.Fail(AuthStatusCode.SessionNotFound, "Incorrect user id");
        }
        catch (Exception)
        {
            return AuthResponse.Fail(AuthStatusCode.UnknownError, "Internal server error");
        }
    }
}