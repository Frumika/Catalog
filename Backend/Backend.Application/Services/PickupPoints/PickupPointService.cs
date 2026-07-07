using Backend.Application.Common;
using Backend.Application.Common.Base;
using Backend.Application.Common.Statuses;
using Backend.Application.Services.PickupPoints.Dtos;
using Backend.Application.Services.PickupPoints.Requests;
using Backend.Domain.Models;

namespace Backend.Application.Services.PickupPoints;

public class PickupPointService
{
    private readonly IBaseRepository _baseRepository;
    private readonly IPickupPointRepository _dbContext;

    public PickupPointService(IBaseRepository baseRepository, IPickupPointRepository dbContext)
    {
        _baseRepository = baseRepository;
        _dbContext = dbContext;
    }

    public async Task<Response> GetPointsAsync(string sessionId)
    {
        try
        {
            int? userId = await _baseRepository.GetUserIdAsync(sessionId);
            if (userId is null)
                return Response.Fail(new UserNotFound(), "The user wasn't found");

            List<PickupPointDto> points = await _dbContext.GetPickupPoints((int)userId);
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
            int? userId = await _baseRepository.GetUserIdAsync(request.UserSessionId);
            if (userId is null)
                return Response.Fail(new UserNotFound(), "The user wasn't found");

            UserPickupPoint? userPoint = await _dbContext.GetUserPickupPoint((int)userId, request.PickupPointId);

            if (userPoint is null)
                return Response.Fail(new PickupPointNotFound(), "The pickup point was not found");

            DateTime currentTime = DateTime.UtcNow;

            userPoint.SelectedAt = currentTime;
            await _baseRepository.CommitAsync();

            return Response.Success(
                new PickupPointDto
                {
                    Id = userPoint.PickupPoint.Id,
                    Address = PickupPointAddressFormatter.Format(userPoint.PickupPoint),
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
            int? userId = await _baseRepository.GetUserIdAsync(request.UserSessionId);
            if (userId is null)
                return Response.Fail(new UserNotFound(), "The user wasn't found");

            await _dbContext.DeleteUserPickupPoint((int)userId, request.PickupPointId);

            return Response.Success("The record was successfully deleted.");
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }
}