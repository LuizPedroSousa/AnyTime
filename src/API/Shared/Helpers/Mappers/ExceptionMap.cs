using AnyTime.Core.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AnyTime.API.Shared.Helpers.Mappers;

public static class MapException
{
  public static IActionResult MapResult(BaseException exception)
  {
    return exception.status switch
    {
      StatusException.BadRequest => new BadRequestObjectResult(exception.message),
      StatusException.NotFound => new NotFoundObjectResult(exception.message)
    };
  }
}