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

    public async Task<Response> GetUserSession(string sessionId)
    {
        if (string.IsNullOrEmpty(sessionId))
            return Response.Fail(new BadRequest(), "Session id is empty");

        try
        {
            UserSessionDto? userSession = await _dbContext.UserSessions
                .AsNoTracking()
                .Where(us => us.UId == sessionId)
                .Select(us => new UserSessionDto
                {
                    SessionId = sessionId,
                    Login = us.User.Login,
                    Email = us.User.Email,
                }).FirstOrDefaultAsync();

            return userSession is not null
                ? Response.Success(userSession)
                : Response.Fail(new UserSessionNotFound(), "User session not found");
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }

    public async Task<Response> SendCodeAsync(SendCodeRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid) return Response.Fail(new BadRequest(), result.Message);

        try
        {
            string emailLower = request.Email.ToLowerInvariant();

            string code = RandomNumberGenerator.GetInt32(100000, 1000000).ToString();
            string hashedCode = Argon2Hasher.HashString(code);

            await _codeStorage.SaveCodeAsync(emailLower, hashedCode);

            var isSuccess = await _verificationSender.SendAsync(emailLower, code);
            return isSuccess
                ? Response.Success("Code was sent successfully")
                : Response.Fail(new UnknownError(), "Failed to send email");
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
            string emailLower = request.Email.ToLowerInvariant();


            string? hashedCode = await _codeStorage.GetCodeAsync(emailLower);
            if (hashedCode == null)
                return Response.Fail(new InvalidVerifyCode(), "Code expired or not found");

            bool isCodeValid = Argon2Hasher.VerifyString(request.Code, hashedCode);
            if (!isCodeValid)
                return Response.Fail(new InvalidVerifyCode(), "Incorrect verification code");

            await _codeStorage.RemoveCodeAsync(emailLower);


            User? user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == emailLower);
            DateTime currentTime = DateTime.UtcNow;
            if (user == null)
            {
                user = new User
                {
                    Email = emailLower,
                    Login = $"User_{Guid.NewGuid():N}".Substring(0, 8), // Чуть более лаконичный Guid
                    CreatedAt = currentTime,
                    Cart = new Cart(),
                    Wishlist = new Wishlist()
                };
                _dbContext.Users.Add(user);
            }

            user.LastLoginAt = currentTime;


            string sessionUId = Guid.NewGuid().ToString();
            UserSession userSession = new()
            {
                UId = sessionUId,
                User = user
            };

            _dbContext.UserSessions.Add(userSession);
            await _dbContext.SaveChangesAsync();

            return Response.Success(new UserSessionDto
            {
                SessionId = sessionUId,
                Login = user.Login,
                Email = user.Email,
            }, "User has been logged in");
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