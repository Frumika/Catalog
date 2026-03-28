using Backend.API.Extensions;
using Backend.Application.DTO.Requests.Review;
using Backend.Application.Services;
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
        return response.ToHttpResponse();
    }

    [HttpPatch("update")]
    public async Task<IActionResult> UpdateReview(UpdateRequest request)
    {
        var response = await _reviewService.UpdateReviewAsync(request);
        return response.ToHttpResponse();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteReview(DeleteRequest request)
    {
        var response = await _reviewService.DeleteReviewAsync(request);
        return response.ToHttpResponse();
    }
    
    [HttpPost("get/list")]
    public async Task<IActionResult> GetReviewList(GetListRequest request)
    {
        var response = await _reviewService.GetReviewListAsync(request);
        return response.ToHttpResponse();
    }
}