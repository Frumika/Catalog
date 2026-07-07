using System.Security.Cryptography;
using Backend.Application.Common;
using Backend.Application.Common.Base;
using Backend.Application.Common.Statuses;
using Backend.Application.DataAccess.Contexts;
using Backend.Application.Services.Auth.Dtos;
using Backend.Application.Services.Auth.Requests;
using Backend.Domain.Interfaces;
using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;


namespace Backend.Application.Services.Auth;

public class AuthService
{
    private readonly MainDbContext _dbContext;
    private readonly IVerificationSender _verificationSender;
    private readonly ICodeStorage _codeStorage;

    public AuthService(MainDbContext dbContext, IVerificationSender verificationSender, ICodeStorage codeStorage)
    {
        _dbContext = dbContext;
        _verificationSender = verificationSender;
        _codeStorage = codeStorage;
    }

    public async Task<Response> SendCodeAsync(SendCodeRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid) return Response.Fail(new BadRequest(), result.Message);

        try
        {
            string code = RandomNumberGenerator.GetInt32(100000, 1000000).ToString();
            string hashedCode = Argon2Hasher.HashString(code);

            await _codeStorage.SaveCodeAsync(request.Email, hashedCode);

            var isSuccess = await _verificationSender.SendAsync(request.Email, code);
            return isSuccess
                ? Response.Success("Code was sent successfully")
                : Response.Fail(new UnknownError(), "Internal server error");
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }

    public async Task<Response> VerifyCodeAsync(VerifyCodeRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid) return Response.Fail(new BadRequest(), result.Message);

        try
        {
            return Response.Success("Placeholder");
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }

    public async Task<Response> RegisterAsync(RegisterRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid) return Response.Fail(new BadRequest(), result.Message);

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
                HashPassword = Argon2Hasher.HashString(request.Password)
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

            if (!Argon2Hasher.VerifyString(request.Password, user.HashPassword))
                return Response.Fail(new InvalidPassword(), "Password is incorrect");

            string sessionUId = Guid.NewGuid().ToString();

            UserSession userSession = new()
            {
                UId = sessionUId,
                UserId = user.Id
            };

            _dbContext.UserSessions.Add(userSession);
            await _dbContext.SaveChangesAsync();

            return Response.Success(new UserSessionDto
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