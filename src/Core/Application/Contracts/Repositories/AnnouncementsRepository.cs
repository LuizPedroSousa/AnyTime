using AnyTime.Core.Domain.Modules.Jobs;
using AnyTime.Core.Domain.Shared;

namespace AnyTime.Core.Application.Contracts.Repositories;

public interface AnnouncementsRepository : GenericRepository<Announcement>
{
  Task<IReadOnlyList<string>> GetUrlsByPlatform(AnnouncementPlatform platform);
  Task<Either<NotFoundException, Announcement>> GetByIdFull(string id);
}