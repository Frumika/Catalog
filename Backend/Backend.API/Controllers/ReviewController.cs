using Backend.API.Extensions;
using Backend.Application.Services.Reviews;
using Backend.Application.Services.Reviews.Requests;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpPost("leave")]
    public async Task<IActionResult> LeaveReview(LeaveRequest request)
    {
        int? userId = User.GetUserId();
        if (userId is null) return Unauthorized();

        var response = await _reviewService.LeaveReviewAsync((int)userId, request);
        return response.ToHttpResponse();
    }

    [Authorize]
    [HttpPatch("update")]
    public async Task<IActionResult> UpdateReview(UpdateRequest request)
    {
        int? userId = User.GetUserId();
        if (userId is null) return Unauthorized();

        var response = await _reviewService.UpdateReviewAsync((int)userId, request);
        return response.ToHttpResponse();
    }

    [Authorize]
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteReview(DeleteRequest request)
    {
        int? userId = User.GetUserId();
        if (userId is null) return Unauthorized();

        var response = await _reviewService.DeleteReviewAsync((int)userId, request);
        return response.ToHttpResponse();
    }


    [HttpPost("get/list")]
    public async Task<IActionResult> GetReviewList(GetListRequest request)
    {
        int? userId = User.GetUserId();

        var response = await _reviewService.GetReviewListAsync(userId, request);
        return response.ToHttpResponse();
    }
}