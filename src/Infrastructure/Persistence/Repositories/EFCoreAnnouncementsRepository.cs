namespace AnyTime.Core.Application.Contracts.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using AnyTime.Core.Domain.Modules.Jobs;
using AnyTime.Core.Domain.Shared;
using AnyTime.Infrastructure.Persistence.DatabaseContext;
using AnyTime.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

public class EFCoreAnnouncementsRepository : EFCoreGenericRepository<Announcement>, AnnouncementsRepository
{
  public EFCoreAnnouncementsRepository(PersistenceDatabaseContext context) : base(context, new NotFoundException("Announcement not found"))
  {
  }

  public async Task<IReadOnlyList<string>> GetUrlsByPlatform(AnnouncementPlatform platform)
  {
    return await (from announcement in _context.announcements where announcement.platform == platform select announcement.url).ToListAsync();
  }

  public async Task<Either<NotFoundException, Announcement>> GetByIdFull(string id)
  {
    return await _context.announcements
      .Where(announcement => announcement.id == id)
      .Include(announcement => announcement.author)
      .FirstOrDefaultAsync();
  }

}

