using MediatR;

namespace AnyTime.Core.Application.Features.Platforms.Queries.GetPlatform;

using AnyTime.Core.Application.Contracts.Repositories;
using AnyTime.Core.Domain.Modules.Platforms;
using AnyTime.Core.Domain.Shared;

public class GetPlatformByNameQueryHandler : IRequestHandler<GetPlatformByNameQuery, Either<NotFoundException, Platform>>
{
  private readonly PlatformsRepository _platformsRepository;
  public GetPlatformByNameQueryHandler(PlatformsRepository platformsRepository)
  {
    _platformsRepository = platformsRepository;
  }

  public async Task<Either<NotFoundException, Platform>> Handle(GetPlatformByNameQuery request, CancellationToken cancellationToken)
  {
    var platformExists = await _platformsRepository.GetByName(request.name);

    if (platformExists.IsLeft())
    {
      return platformExists.left;
    }

    return platformExists.right;
  }
}