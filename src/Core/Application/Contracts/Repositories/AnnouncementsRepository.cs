using AnyTime.Core.Domain.Modules.Jobs;
using AnyTime.Core.Domain.Modules.Jobs.Exceptions;

namespace AnyTime.Core.Application.Contracts.Repositories;

public interface AnnouncementsRepository : GenericRepository<AnnouncementNotFoundException, Announcement>
{
  Task<IReadOnlyList<string>> GetUrls();
}