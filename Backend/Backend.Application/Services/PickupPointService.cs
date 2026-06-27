using Backend.Application.DTO.PickupPoint;
using Backend.Application.Requests.Base;
using Backend.Application.Requests.PickupPoint;
using Backend.Application.Responses;
using Backend.Application.Statuses;
using Backend.DataAccess.Postgres.Contexts;
using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Backend.Application.Services;

public class PickupPointService
{
    private readonly MainDbContext _dbContext;

    public PickupPointService(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response> GetPointsAsync(GetPickupPointsRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid)
            return Response.Fail(new BadRequest(), result.Message);

        try
        {
            int? userId = await _dbContext.UserSessions
                .AsNoTracking()
                .Where(ui => ui.UId == request.UserSessionId)
                .Select(ui => (int?)ui.UserId)
                .FirstOrDefaultAsync();
            if (userId is null)
                return Response.Fail(new UserNotFound(), "The user wasn't found");

            List<PickupPointDto> points = await _dbContext.UserPickupPoints
                .AsNoTracking()
                .Where(upp => upp.UserId == userId)
                .Select(upp =>
                    new PickupPointDto
                    {
                        Id = upp.PickupPoint.Id,
                        Address = MakePickupPointAddress(upp.PickupPoint),
                        ShelfLifetime = upp.PickupPoint.ShelfLifetime,
                        SelectedAt = upp.SelectedAt
                    })
                .ToListAsync();

            return Response.Success(points);
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }

    public async Task<Response> UpdateSelectedPointAsync(UpdateSelectedPointRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid)
            return Response.Fail(new BadRequest(), result.Message);

        try
        {
            int? userId = await _dbContext.UserSessions
                .AsNoTracking()
                .Where(ui => ui.UId == request.UserSessionId)
                .Select(ui => (int?)ui.UserId)
                .FirstOrDefaultAsync();
            if (userId is null)
                return Response.Fail(new UserNotFound(), "The user wasn't found");

            UserPickupPoint? userPoint = await _dbContext.UserPickupPoints
                .Where(upp =>
                    upp.UserId == userId &&
                    upp.PickupPointId == request.PickupPointId
                )
                .Include(upp => upp.PickupPoint)
                .FirstOrDefaultAsync();

            if (userPoint is null)
                return Response.Fail(new PickupPointNotFound(), "The pickup point was not found");

            DateTime currentTime = DateTime.Now;

            userPoint.SelectedAt = currentTime;
            await _dbContext.SaveChangesAsync();

            return Response.Success(
                new PickupPointDto
                {
                    Id = userPoint.PickupPoint.Id,
                    Address = MakePickupPointAddress(userPoint.PickupPoint),
                    ShelfLifetime = userPoint.PickupPoint.ShelfLifetime,
                    SelectedAt = currentTime
                });
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }

    public async Task<Response> RemoveById(int id)
    {
        return Response.Success();
    }

    private string MakePickupPointAddress(PickupPoint point)
    {
        return string.Empty;
    }
}