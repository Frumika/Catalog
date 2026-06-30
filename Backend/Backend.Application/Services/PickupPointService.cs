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

    public async Task<Response> GetPointsAsync(string sessionId)
    {
        try
        {
            int? userId = await _dbContext.UserSessions
                .AsNoTracking()
                .Where(ui => ui.UId == sessionId)
                .Select(ui => (int?)ui.UserId)
                .FirstOrDefaultAsync();
            if (userId is null)
                return Response.Fail(new UserNotFound(), "The user wasn't found");

            var rawPoints = await _dbContext.UserPickupPoints
                .AsNoTracking()
                .Where(upp => upp.UserId == userId)
                .Select(upp => new
                {
                    upp.SelectedAt,
                    PickupPoint = upp.PickupPoint
                })
                .ToListAsync();

            List<PickupPointDto> points = rawPoints
                .Select(x =>
                    new PickupPointDto
                    {
                        Id = x.PickupPoint.Id,
                        Address = MakePickupPointAddress(x.PickupPoint),
                        ShelfLifetime = x.PickupPoint.ShelfLifetime,
                        SelectedAt = x.SelectedAt
                    }
                )
                .OrderBy(x => x.SelectedAt)
                .ToList();

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

            DateTime currentTime = DateTime.UtcNow;

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

    public async Task<Response> RemovePointAsync(RemovePointRequest request)
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

            await _dbContext.UserPickupPoints
                .Where(upp => upp.UserId == userId && upp.PickupPointId == request.PickupPointId)
                .ExecuteDeleteAsync();

            return Response.Success("The record was successfully deleted.");
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }

    private string MakePickupPointAddress(PickupPoint point)
    {
        var streetAbbr = point.StreetType switch
        {
            StreetType.Street => "ул.",
            StreetType.Avenue => "пр.",
            StreetType.Lane => "пер.",
            StreetType.Boulevard => "б-р",
            StreetType.Highway => "ш.",
            StreetType.Square => "пл.",
            StreetType.Embankment => "наб.",
            StreetType.Passage => "проезд",
            StreetType.Alley => "ал.",
            _ => point.StreetType.ToString()
        };

        var address = $"г. {point.City}, {streetAbbr} {point.StreetName}, д. {point.House}";

        if (!string.IsNullOrEmpty(point.Building))
            address += $", корп. {point.Building}";

        return address;
    }
}