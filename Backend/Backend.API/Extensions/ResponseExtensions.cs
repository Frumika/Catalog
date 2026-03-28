using System.Net;
using Backend.Application.Errors;
using Backend.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Extensions;

public static class ResponseExtensions
{
    public static IActionResult ToHttpResponse(this Response response)
    {
        if (response.IsSuccess) return new OkObjectResult(response);

        return response.Error switch
        {
            BadRequest => new BadRequestObjectResult(response),
            NotFound => new NotFoundObjectResult(response),
            Unauthorized => new UnauthorizedObjectResult(response),
            Conflict => new ConflictObjectResult(response),
            UnknownError => new ObjectResult(response) { StatusCode = 500 },
            _ => new ObjectResult(response) { StatusCode = 500 }
        };
    }
}