using Backend.Application.Common;
using Backend.Application.Common.Statuses;
using Backend.Application.DataAccess.Contexts;
using Backend.Application.Services.Users.Dtos;
using Microsoft.EntityFrameworkCore;


namespace Backend.Application.Services.Users;

public class UserService
{
    private readonly MainDbContext _dbContext;

    public UserService(MainDbContext dbContext)
    {
        _dbContext = dbContext;
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
}