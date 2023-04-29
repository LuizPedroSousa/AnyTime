using AnyTime.Core.Domain.Modules.Jobs;

namespace AnyTime.Core.Application.Contracts.Repositories;

public interface JobsRepository : GenericRepository<Job>
{
  Task<IReadOnlyList<Job>> GetAllByStatus(JobStatus status);
}