using Backend.Application.DTO.Entities.Review;
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
                .Where(us => us.UId == request.UserSessionId)
                .Select(us => (int?)us.UserId)
                .FirstOrDefaultAsync();
            if (userId is null)
                return ReviewResponse.Fail(ReviewStatusCode.UserNotFound, "The user wasn't found");

            bool isProductWasOrdered = await _dbContext.OrderedProducts
                .AnyAsync(op => op.ProductId == request.ProductId && op.Order.UserId == userId);
            if (!isProductWasOrdered)
                return ReviewResponse.Fail(ReviewStatusCode.ProductNotFound, "The product wasn't purchased");

            Review review = new()
            {
                UserId = (int)userId,
                ProductId = request.ProductId,
                Score = request.Score,
                Text = request.Text,
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.Add(review);
            await _dbContext.SaveChangesAsync();

            return ReviewResponse.Success(new ReviewDto
            {
                Id = review.Id,
                Score = review.Score,
                Text = review.Text,
                CreatedAt = review.CreatedAt,
                UpdatedAt = review.UpdatedAt
            });
        }
        catch (Exception)
        {
            return ReviewResponse.Fail(ReviewStatusCode.UnknownError, "Interval server error");
        }
    }

    public async Task<ReviewResponse> UpdateReviewAsync(UpdateRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid)
            return ReviewResponse.Fail(ReviewStatusCode.BadRequest, result.Message);

        try
        {
            int? userId = await _dbContext.UserSessions
                .Where(us => us.UId == request.UserSessionId)
                .Select(us => (int?)us.UserId)
                .FirstOrDefaultAsync();
            if (userId is null)
                return ReviewResponse.Fail(ReviewStatusCode.UserNotFound, "The user wasn't found");

            Review? review = await _dbContext.Reviews
                .FirstOrDefaultAsync(r => r.Id == request.ReviewId && r.UserId == userId);
            if (review is null)
                return ReviewResponse.Fail(ReviewStatusCode.ReviewNotFound, "The review wasn't found");

            bool isChanged = false;
            if (review.Score != request.Score)
            {
                isChanged = true;
                review.Score = request.Score;
            }

            if (!string.Equals(review.Text, request.Text, StringComparison.Ordinal))
            {
                isChanged = true;
                review.Text = request.Text;
            }

            if (isChanged)
            {
                review.UpdatedAt = DateTime.UtcNow;
                await _dbContext.SaveChangesAsync();
            }

            return ReviewResponse.Success(new ReviewDto
            {
                Id = review.Id,
                Score = review.Score,
                Text = review.Text,
                CreatedAt = review.CreatedAt,
                UpdatedAt = review.UpdatedAt
            });
        }
        catch (Exception)
        {
            return ReviewResponse.Fail(ReviewStatusCode.UnknownError, "Internal server error");
        }
    }
}