using AnyTime.Core.Application.Contracts.Repositories;
using AnyTime.Core.Domain.Modules.Jobs;
using AnyTime.Core.Domain.Modules.Jobs.Exceptions;
using AnyTime.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace AnyTime.Infrastructure.Persistence.Repositories;

public class EFCoreJobsRepository : EFCoreGenericRepository<JobNotFoundException, Job>, JobsRepository
{
  public EFCoreJobsRepository(PersistenceDatabaseContext context) : base(context)
  {
  }

  public async Task<IReadOnlyList<Job>> GetAllByStatus(JobStatus status)
  {
    return await _context.jobs.Where(job => job.status == status).ToListAsync();
  }
}
