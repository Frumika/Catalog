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


namespace Backend.Application.Services.Auth;

public class AuthService
{
    private readonly MainDbContext _dbContext;
    private readonly IVerificationSender _verificationSender;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly ICodeStorage _codeStorage;

    public AuthService(MainDbContext dbContext, ITokenGenerator tokenGenerator,
        IVerificationSender verificationSender, ICodeStorage codeStorage)
    {
        _dbContext = dbContext;
        _tokenGenerator = tokenGenerator;
        _verificationSender = verificationSender;
        _codeStorage = codeStorage;
    }

    public async Task<Response> GetUserAsync(int userId)
    {
        try
        {
            UserDto? userSession = await _dbContext.Users
                .AsNoTracking()
                .Where(u => u.Id == userId)
                .Select(u => new UserDto
                {
                    Login = u.Login,
                    Email = u.Email,
                }).FirstOrDefaultAsync();

            return userSession is not null
                ? Response.Success(userSession)
                : Response.Fail(new TokenNotFound(), "User session not found");
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


            string accessToken = _tokenGenerator.GenerateAccessToken(user);
            RefreshToken refreshToken = _tokenGenerator.GenerateRefreshToken(user);

            _dbContext.RefreshTokens.Add(refreshToken);
            await _dbContext.SaveChangesAsync();

            return Response.Success(
                new SessionDto()
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken.Token,
                }, "User has been logged in");
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }

    public async Task<Response> RefreshAccessTokenAsync(RefreshRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid) return Response.Fail(new BadRequest(), result.Message);

        try
        {
            RefreshToken? refreshToken = await _dbContext.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken);

            if (refreshToken is null)
                return Response.Fail(new TokenNotFound(), "Invalid refresh token");

            if (refreshToken.IsRevoked)
                return Response.Fail(new TokenRevoked(), "Refresh token revoked");

            if (refreshToken.ExpiresAt <= DateTime.UtcNow)
            {
                refreshToken.IsRevoked = true;
                await _dbContext.SaveChangesAsync();

                return Response.Fail(new TokenExpired(), "Refresh token expired");
            }

            string accessToken = _tokenGenerator.GenerateAccessToken(refreshToken.User);

            return Response.Success(
                new SessionDto
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken.Token,
                }, "Access token has been refreshed"
            );
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
            await _dbContext.RefreshTokens
                .Where(us => us.Token == request.RefreshToken)
                .ExecuteDeleteAsync();

            return Response.Success("The user has been logged out");
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }

    public async Task<Response> LogoutAllSessionsAsync(int userId)
    {
        try
        {
            await _dbContext.RefreshTokens
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