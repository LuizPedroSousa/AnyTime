using AnyTime.Core.Application.Contracts.Repositories;
using AnyTime.Core.Domain.Modules.Platforms;
using AnyTime.Core.Domain.Shared;
using AnyTime.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace AnyTime.Infrastructure.Persistence.Repositories;

public class EFCorePlatformsRepository : EFCoreGenericRepository<Platform>, PlatformsRepository
{
  public EFCorePlatformsRepository(PersistenceDatabaseContext context) : base(context, new NotFoundException("Platform not found"))
  {
  }

  public async Task<Either<NotFoundException, Platform>> GetByName(string name)
  {
    return await (from platform in _context.platforms where platform.name == name select platform).FirstOrDefaultAsync();
  }
}