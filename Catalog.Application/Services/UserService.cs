using Catalog.Application.DTO;
using Catalog.Application.Interfaces;
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

    public async Task<UserDbResponse> LoginAsync(LoginRequest request)
    {
        User? user = null;
        UserDbResponse response = new()
        {
            IsSuccess = true,
            Message = "User logged in"
        };

        try
        {
            user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Login == request.Login);
        }
        catch (Exception e)
        {
            response.IsSuccess = false;
            response.Message = e.Message;
        }

        if (user is null)
        {
            response.IsSuccess = false;
            response.Message = "User not found";
        }

        else if (!IsPasswordsEquals(user.Password, request.Password))
        {
            response.IsSuccess = false;
            response.Message = "Password is incorrect";
        }

        return response;
    }


    public async Task<UserDbResponse> RegisterAsync(RegisterRequest request)
    {
        bool isUserExist = false;

        UserDbResponse response = new()
        {
            IsSuccess = true,
            Message = "User registered"
        };

        try
        {
            isUserExist = await _dbContext.Users
                .AsNoTracking()
                .AnyAsync(user => user.Login == request.Login || user.Email == request.Email);
        }
        catch (Exception e)
        {
            response.IsSuccess = false;
            response.Message = e.Message;
        }

        if (isUserExist)
        {
            response.IsSuccess = false;
            response.Message = "User already exists";
            return response;
        }

        User user = new()
        {
            Email = request.Email,
            Login = request.Login,
            Password = request.Password
        };
        _dbContext.Add(user);

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            response.IsSuccess = false;
            response.Message = e.Message;
        }


        return response;
    }

    private bool IsPasswordsEquals(string firstPassword, string secondPassword) =>
        string.Equals(firstPassword, secondPassword);
}