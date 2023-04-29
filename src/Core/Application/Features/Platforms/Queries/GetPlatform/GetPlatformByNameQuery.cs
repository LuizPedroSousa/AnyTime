using AnyTime.Core.Domain.Modules.Platforms;
using AnyTime.Core.Domain.Shared;
using MediatR;

namespace AnyTime.Core.Application.Features.Platforms.Queries;

public class GetPlatformByNameQuery : IRequest<Either<NotFoundException, Platform>>
{
  public string name { get; set; }
}