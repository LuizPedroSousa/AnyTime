using AnyTime.Core.Domain.Modules.Announcements;
using AnyTime.Core.Domain.Modules.Platforms;
using AnyTime.Core.Domain.Shared;

namespace AnyTime.Core.Application.Contracts.Repositories;

public interface AnnouncementsRepository : GenericRepository<Announcement>
{
  Task<IReadOnlyList<string>> GetUrlsByPlatform(Platform platform);
  Task<IReadOnlyList<Announcement>> GetWithPlatform();
  Task<Either<NotFoundException, Announcement>> GetByIdFull(string id);
}