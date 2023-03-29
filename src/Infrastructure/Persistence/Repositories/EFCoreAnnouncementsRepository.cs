namespace AnyTime.Core.Application.Contracts.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using AnyTime.Core.Domain.Modules.Jobs;
using AnyTime.Core.Domain.Modules.Jobs.Exceptions;
using AnyTime.Infrastructure.Persistence.DatabaseContext;
using AnyTime.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

public class EFCoreAnnouncementsRepository : EFCoreGenericRepository<AnnouncementNotFoundException, Announcement>, AnnouncementsRepository
{
  public EFCoreAnnouncementsRepository(PersistenceDatabaseContext context) : base(context)
  {
  }

  public async Task<IReadOnlyList<string>> GetUrls()
  {
    return await (from announcement in _context.announcements select announcement.url).ToListAsync();
  }
}