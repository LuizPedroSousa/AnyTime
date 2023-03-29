using AnyTime.Core.Domain.Modules.Jobs;
using AnyTime.Core.Domain.Modules.Jobs.Exceptions;

namespace AnyTime.Core.Application.Contracts.Repositories;

public interface JobsRepository : GenericRepository<JobNotFoundException, Job>
{
  Task<IReadOnlyList<Job>> GetAllByStatus(JobStatus status);
}