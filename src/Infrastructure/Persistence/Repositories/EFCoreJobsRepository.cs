using AnyTime.Core.Application.Contracts.Repositories;
using AnyTime.Core.Domain.Modules.Announcements;
using AnyTime.Core.Domain.Shared;
using AnyTime.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace AnyTime.Infrastructure.Persistence.Repositories;

public class EFCoreJobsRepository : EFCoreGenericRepository<Job>, JobsRepository
{
  public EFCoreJobsRepository(PersistenceDatabaseContext context) : base(context, new NotFoundException("Job not found"))
  {
  }

  public async Task<IReadOnlyList<Job>> GetAllByStatus(JobStatus status)
  {
    return await _context.jobs.Where(job => job.status == status).ToListAsync();
  }
}
