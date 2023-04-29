using AnyTime.Core.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AnyTime.API.Shared.Helpers.Mappers;

public static class MapException
{
  private static object MapResponse(BaseException exception)
  {
    return new
    {
      statusCode = exception.status,
      message = exception.message
    };
  }

  public static IActionResult MapResult(BaseException exception)
  {
    return exception.status switch
    {
      StatusException.BadRequest => new BadRequestObjectResult(MapResponse(exception)),
      StatusException.NotFound => new NotFoundObjectResult(MapResponse(exception))
    };
  }
}