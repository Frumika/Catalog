using Backend.Application.DTO.Entities.Review;
using Backend.Application.DTO.Requests.Base;
using Backend.Application.DTO.Requests.Review;
using Backend.Application.DTO.Responses;
using Backend.Application.Errors;
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


    public async Task<Response> LeaveReviewAsync(LeaveRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid)
            return Response.Fail(new BadRequest(), result.Message);

        try
        {
            int? userId = await _dbContext.UserSessions
                .Where(us => us.UId == request.UserSessionId)
                .Select(us => (int?)us.UserId)
                .FirstOrDefaultAsync();
            if (userId is null)
                return Response.Fail(new UserNotFound(), "The user wasn't found");

            bool isProductWasOrdered = await _dbContext.OrderedProducts
                .AnyAsync(op => op.ProductId == request.ProductId && op.Order.UserId == userId);
            if (!isProductWasOrdered)
                return Response.Fail(new ProductNotFound(), "The product wasn't purchased");

            bool isReviewAlreadyExist = await _dbContext.Reviews
                .AnyAsync(r => r.ProductId == request.ProductId && r.UserId == userId);
            if (isReviewAlreadyExist)
                return Response.Fail(new ReviewAlreadyExists(), "The review was already existed");

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

            return Response.Success(
                new ReviewDto
                {
                    Id = review.Id,
                    Score = review.Score,
                    Text = review.Text,
                    CreatedAt = review.CreatedAt,
                    UpdatedAt = review.UpdatedAt
                }
            );
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Interval server error");
        }
    }

    public async Task<Response> UpdateReviewAsync(UpdateRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid)
            return Response.Fail(new BadRequest(), result.Message);

        try
        {
            int? userId = await _dbContext.UserSessions
                .Where(us => us.UId == request.UserSessionId)
                .Select(us => (int?)us.UserId)
                .FirstOrDefaultAsync();
            if (userId is null)
                return Response.Fail(new UserNotFound(), "The user wasn't found");

            Review? review = await _dbContext.Reviews
                .FirstOrDefaultAsync(r => r.Id == request.ReviewId && r.UserId == userId);
            if (review is null)
                return Response.Fail(new ReviewNotFound(), "The review wasn't found");

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

            return Response.Success(
                new ReviewDto
                {
                    Id = review.Id,
                    Score = review.Score,
                    Text = review.Text,
                    CreatedAt = review.CreatedAt,
                    UpdatedAt = review.UpdatedAt
                }
            );
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }

    public async Task<Response> DeleteReviewAsync(DeleteRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid)
            return Response.Fail(new BadRequest(), result.Message);

        try
        {
            int? userId = await _dbContext.UserSessions
                .Where(us => us.UId == request.UserSessionId)
                .Select(us => (int?)us.UserId)
                .FirstOrDefaultAsync();
            if (userId is null)
                return Response.Fail(new UserNotFound(), "The user wasn't found");

            await _dbContext.Reviews
                .Where(r => r.UserId == userId && r.Id == request.ReviewId)
                .ExecuteDeleteAsync();

            return Response.Success("The review was deleted");
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }

    public async Task<Response> GetReviewListAsync(GetListRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid)
            return Response.Fail(new BadRequest(), result.Message);

        try
        {
            int? userId = await _dbContext.UserSessions
                .Where(us => us.UId == request.UserSessionId)
                .Select(us => (int?)us.UserId)
                .FirstOrDefaultAsync();

            int pageNumber = request.PageNumber - 1;

            List<ReviewDto> reviews = await _dbContext.Reviews
                .AsNoTracking()
                .Where(r => r.ProductId == request.ProductId)
                .OrderByDescending(r => r.UserId == userId)
                .ThenByDescending(r => r.CreatedAt)
                .Skip(pageNumber * request.PageSize)
                .Take(request.PageSize)
                .Select(r => new ReviewDto
                {
                    Score = r.Score,
                    Text = r.Text,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt,
                    UserLogin = r.User.Login
                })
                .ToListAsync();

            int totalCount = await _dbContext.Reviews.CountAsync(r => r.ProductId == request.ProductId);
            return Response.Success(new ReviewListDto { Reviews = reviews, TotalCount = totalCount });
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }
}