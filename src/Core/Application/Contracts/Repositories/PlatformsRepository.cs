using AnyTime.Core.Domain.Modules.Platforms;
using AnyTime.Core.Domain.Shared;

namespace AnyTime.Core.Application.Contracts.Repositories;

public interface PlatformsRepository : GenericRepository<Platform>
{
  Task<Either<NotFoundException, Platform>> GetByName(string name);
}
