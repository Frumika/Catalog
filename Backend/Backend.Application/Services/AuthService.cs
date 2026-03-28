using Backend.Application.DTO.Requests.Auth;
using Backend.Application.DTO.Requests.Base;
using Backend.Application.DTO.Responses;
using Backend.Application.Errors;
using Backend.Application.Exceptions;
using Backend.Application.Logic;
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

    public async Task<Response> RegisterAsync(RegisterRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid) 
            return Response.Fail(new BadRequest());

        await using IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            bool isUserExist = await _dbContext.Users
                .AsNoTracking()
                .AnyAsync(user => user.Login == request.Login);

            if (isUserExist)
                throw new ServiceException(new UserAlreadyExists(), "User already exists");

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

            return Response.Success("User registered");
        }
        catch (ServiceException e)
        {
            await transaction.RollbackAsync();
            return Response.Fail(e.Error, e.Message);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }

    public async Task<Response> LoginAsync(LoginRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid)
            return Response.Fail(new BadRequest(), result.Message);

        try
        {
            User? user = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Login == request.Login);

            if (user is null)
                return Response.Fail(new UserNotFound(), "The user wasn't found");

            if (!Argon2Hasher.VerifyPassword(request.Password, user.HashPassword))
                return Response.Fail(new InvalidPassword(), "Password is incorrect");

            string sessionUId = Guid.NewGuid().ToString();

            UserSession userSession = new()
            {
                UId = sessionUId,
                UserId = user.Id
            };

            _dbContext.UserSessions.Add(userSession);
            await _dbContext.SaveChangesAsync();

            return Response.Success(new DTO.Entities.Auth.UserSessionDto
            {
                SessionId = sessionUId,
                Login = user.Login
            }, "User has beel logged in");
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }

    public async Task<Response> LogoutSessionAsync(LogoutRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid)
            return Response.Fail(new BadRequest(), result.Message);

        try
        {
            await _dbContext.UserSessions
                .Where(us => us.UId == request.SessionId)
                .ExecuteDeleteAsync();

            return Response.Success("The user has been logged out");
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }

    public async Task<Response> LogoutAllSessionsAsync(LogoutRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid)
            return Response.Fail(new BadRequest(), result.Message);

        try
        {
            int? userId = await _dbContext.UserSessions
                .AsNoTracking()
                .Where(us => us.UId == request.SessionId)
                .Select(us => (int?)us.UserId)
                .FirstOrDefaultAsync();

            if (userId is null)
                return Response.Fail(new UserNotFound(), "User not found");

            await _dbContext.UserSessions
                .Where(us => us.UserId == userId)
                .ExecuteDeleteAsync();

            return Response.Success("The user has been logged out of all sessions");
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }
}