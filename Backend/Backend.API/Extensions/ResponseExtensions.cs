using Backend.Application.Responses;
using Backend.Application.Statuses;
using Microsoft.AspNetCore.Mvc;


namespace Backend.API.Extensions;

public static class ResponseExtensions
{
    public static IActionResult ToHttpResponse(this Response response)
    {
        return response.Status switch
        {
            Success => new OkObjectResult(response),
            BadRequest => new BadRequestObjectResult(response),
            NotFound => new NotFoundObjectResult(response),
            Unauthorized => new UnauthorizedObjectResult(response),
            Conflict => new ConflictObjectResult(response),
            UnknownError => new ObjectResult(response) { StatusCode = 500 },
            _ => new ObjectResult(response) { StatusCode = 500 }
        };
    }
}