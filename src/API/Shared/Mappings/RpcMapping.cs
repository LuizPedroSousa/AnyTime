using AnyTime.Core.Domain.Shared;
using Grpc.Core;

namespace AnyTime.API.Shared.Mappings;

public static class RpcMapping
{
  public static RpcException MapException(BaseException exception)
  {

    var statusCode = exception.status switch
    {
      StatusException.NotFound => StatusCode.NotFound,
      StatusException.BadRequest => StatusCode.InvalidArgument,
      _ => StatusCode.Internal
    };

    var metadata = new Metadata {
      {"message", exception.message}
    };

    return new RpcException(new Status(statusCode, exception.message), metadata);
  }
}