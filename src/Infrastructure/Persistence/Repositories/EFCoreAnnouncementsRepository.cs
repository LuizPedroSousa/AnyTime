namespace AnyTime.Core.Application.Contracts.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using AnyTime.Core.Domain.Modules.Announcements;
using AnyTime.Core.Domain.Modules.Platforms;
using AnyTime.Core.Domain.Shared;
using AnyTime.Infrastructure.Persistence.DatabaseContext;
using AnyTime.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

public class EFCoreAnnouncementsRepository : EFCoreGenericRepository<Announcement>, AnnouncementsRepository
{
  public EFCoreAnnouncementsRepository(PersistenceDatabaseContext context) : base(context, new NotFoundException("Announcement not found"))
  {
  }

  public async Task<IReadOnlyList<string>> GetUrlsByPlatform(Platform platform)
  {
    return await (from announcement in _context.announcements where announcement.platform.id == platform.id select announcement.url).ToListAsync();
  }


  public async Task<Either<NotFoundException, Announcement>> GetByIdFull(string id)
  {
    return await _context.announcements
      .Where(announcement => announcement.id == id)
      .Include(announcement => announcement.author)
      .Include(announcement => announcement.platform)
      .FirstOrDefaultAsync();
  }

  public async Task<IReadOnlyList<Announcement>> GetWithPlatform()
  {
    return await _context.announcements
      .Include(announcement => announcement.platform)
      .OrderByDescending(announcement => announcement.publishedAt)
      .ToListAsync();
  }
}

