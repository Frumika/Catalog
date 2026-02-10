using Backend.Application.DTO.Requests.Auth;
using Backend.Application.DTO.Responses;
using Backend.Application.Exceptions;
using Backend.Application.Logic;
using Backend.Application.StatusCodes;
using Backend.DataAccess.Postgres.Contexts;
using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;


namespace Backend.Application.Services;

public class AuthService
{
    private readonly MainDbContext _dbContext;

    public AuthService(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var validationResult = request.Validate();
        if (!validationResult.IsValid)
            return AuthResponse.Fail(AuthStatusCode.BadRequest, validationResult.Message);

        await using IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            bool isUserExist = await _dbContext.Users
                .AsNoTracking()
                .AnyAsync(user => user.Login == request.Login);

            if (isUserExist)
                throw new AuthException(AuthStatusCode.UserAlreadyExists, "User already exists");

            User user = new()
            {
                Login = request.Login,
                HashPassword = Argon2Hasher.HashPassword(request.Password)
            };

            Cart cart = new() { User = user };
            Wishlist wishlist = new() { User = user };

            _dbContext.Add(user);
            _dbContext.Add(cart);
            _dbContext.Add(wishlist);

            await transaction.CommitAsync();
            await _dbContext.SaveChangesAsync();

            return AuthResponse.Success("User registered");
        }
        catch (AuthException e)
        {
            await transaction.RollbackAsync();
            return AuthResponse.Fail(e.StatusCode, e.Message);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            return AuthResponse.Fail(AuthStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var validationResult = request.Validate();
        if (!validationResult.IsValid)
            return AuthResponse.Fail(AuthStatusCode.BadRequest, validationResult.Message);

        try
        {
            User? user = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Login == request.Login);

            if (user is null)
                return AuthResponse.Fail(AuthStatusCode.UserNotFound, "The user wasn't found");

            if (!Argon2Hasher.VerifyPassword(request.Password, user.HashPassword))
                return AuthResponse.Fail(AuthStatusCode.InvalidPassword, "Password is incorrect");

            string sessionUId = Guid.NewGuid().ToString();

            UserSession userSession = new()
            {
                UId = sessionUId,
                User = user
            };

            _dbContext.UserSessions.Add(userSession);
            await _dbContext.SaveChangesAsync();

            return AuthResponse.Success(new DTO.Entities.Auth.UserSessionDto
            {
                SessionId = sessionUId,
                Login = user.Login
            }, "User has beel logged in");
        }
        catch (Exception)
        {
            return AuthResponse.Fail(AuthStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<AuthResponse> LogoutSessionAsync(LogoutRequest request)
    {
        var validationResult = request.Validate();
        if (!validationResult.IsValid)
            return AuthResponse.Fail(AuthStatusCode.BadRequest, validationResult.Message);

        try
        {
            await _dbContext.UserSessions
                .Where(us => us.UId == request.SessionId)
                .ExecuteDeleteAsync();

            return AuthResponse.Success("The user has been logged out");
        }
        catch (Exception)
        {
            return AuthResponse.Fail(AuthStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<AuthResponse> LogoutAllSessionsAsync(LogoutRequest request)
    {
        var validationResult = request.Validate();
        if (!validationResult.IsValid)
            return AuthResponse.Fail(AuthStatusCode.BadRequest, validationResult.Message);

        try
        {
            int? userId = await _dbContext.UserSessions
                .AsNoTracking()
                .Where(us => us.UId == request.SessionId)
                .Select(us => (int?)us.UserId)
                .FirstOrDefaultAsync();

            if (userId is null)
                return AuthResponse.Fail(AuthStatusCode.UserNotFound, "User not found");

            await _dbContext.UserSessions
                .Where(us => us.UserId == userId)
                .ExecuteDeleteAsync();

            return AuthResponse.Success("The user has been logged out of all sessions");
        }
        catch (Exception)
        {
            return AuthResponse.Fail(AuthStatusCode.UnknownError, "Internal server error");
        }
    }
}