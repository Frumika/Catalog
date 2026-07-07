using Backend.Application.Common;
using Backend.Application.Common.Base;
using Backend.Application.Common.Statuses;
using Backend.Application.DataAccess.Contexts;
using Backend.Application.Services.Auth.Requests;

namespace Backend.Application.Services.Auth;

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
            return Response.Fail(new BadRequest(), result.Message);

        try
        {
            return Response.Success("User registered");
        }
        catch (ServiceException e)
        {
            return Response.Fail(e.Error, e.Message);
        }
        catch (Exception)
        {
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
            return Response.Success("User has beel logged in");
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
            // await _dbContext.UserSessions
            //     .Where(us => us.UId == request.SessionId)
            //     .ExecuteDeleteAsync();

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
            // int? userId = await _authRepository.UserSessions
            //     .AsNoTracking()
            //     .Where(us => us.UId == request.SessionId)
            //     .Select(us => (int?)us.UserId)
            //     .FirstOrDefaultAsync();
            //
            // if (userId is null)
            //     return Response.Fail(new UserNotFound(), "User not found");
            //
            // await _authRepository.UserSessions
            //     .Where(us => us.UserId == userId)
            //     .ExecuteDeleteAsync();

            return Response.Success("The user has been logged out of all sessions");
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }
}