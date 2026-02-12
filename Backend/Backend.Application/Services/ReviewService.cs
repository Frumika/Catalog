using Backend.Application.DTO.Requests.Base;
using Backend.Application.DTO.Requests.Review;
using Backend.Application.DTO.Responses;
using Backend.Application.StatusCodes;
using Backend.DataAccess.Postgres.Contexts;
using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Backend.Application.Services;

public class ReviewService
{
    private readonly MainDbContext _dbContext;

    public ReviewService(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<ReviewResponse> LeaveReviewAsync(LeaveRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid)
            return ReviewResponse.Fail(ReviewStatusCode.BadRequest, result.Message);

        try
        {
            int? userId = await _dbContext.UserSessions
                .AsNoTracking()
                .Where(us => us.UId == request.UserSessionId)
                .Select(us => (int?)us.UserId)
                .FirstOrDefaultAsync();
            if (userId is null)
                return ReviewResponse.Fail(ReviewStatusCode.UserNotFound, "The user wasn't found");

            int? productId = await _dbContext.Products
                .AsNoTracking()
                .Where(p => p.Id == request.ProductId)
                .Select(p => (int?)p.Id)
                .FirstOrDefaultAsync();
            if (productId is null)
                return ReviewResponse.Fail(ReviewStatusCode.ProductNotFound, "The product wasn't found");

            bool isProductWasOrdered = await _dbContext.OrderedProducts
                .AsNoTracking()
                .AnyAsync(op => op.ProductId == productId && op.Order.UserId == userId);
            if (!isProductWasOrdered)
                return ReviewResponse.Fail(ReviewStatusCode.ProductNotFound, "The product wasn't purchased");

            Review review = new()
            {
                UserId = (int)userId,
                ProductId = (int)productId,
                Score = request.Score,
                Text = request.Text,
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.Add(review);
            await _dbContext.SaveChangesAsync();
            
            return ReviewResponse.Success("The review was left");
        }
        catch (Exception)
        {
            return ReviewResponse.Fail(ReviewStatusCode.UnknownError, "Interval server error");
        }
    }
    
}