using Backend.Application.DTO.Requests.Review;
using Backend.Application.DTO.Responses;
using Backend.Application.Services;
using Backend.Application.StatusCodes;
using Microsoft.AspNetCore.Mvc;


namespace Backend.API.Controllers;

[ApiController]
[Route("api/review")]
public class ReviewController : ControllerBase
{
    private readonly ReviewService _reviewService;

    public ReviewController(ReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpPost("leave")]
    public async Task<IActionResult> LeaveReview(LeaveRequest request)
    {
        var response = await _reviewService.LeaveReviewAsync(request);
        return ToHttpResponse(response);
    }

    [HttpPatch("update")]
    public async Task<IActionResult> UpdateReview(UpdateRequest request)
    {
        var response = await _reviewService.UpdateReviewAsync(request);
        return ToHttpResponse(response);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteReview(DeleteRequest request)
    {
        var response = await _reviewService.DeleteReviewAsync(request);
        return ToHttpResponse(response);
    }

    private IActionResult ToHttpResponse(ReviewResponse response)
    {
        return response.Code switch
        {
            ReviewStatusCode.Success => Ok(response),

            ReviewStatusCode.UserNotFound => NotFound(response),
            ReviewStatusCode.ProductNotFound => NotFound(response),
            ReviewStatusCode.ReviewNotFound => NotFound(response),

            ReviewStatusCode.BadRequest => BadRequest(response),

            ReviewStatusCode.UnknownError => StatusCode(StatusCodes.Status500InternalServerError, response),

            _ => BadRequest(response)
        };
    }
}