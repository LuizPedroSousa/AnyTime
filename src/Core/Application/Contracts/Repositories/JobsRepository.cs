using AnyTime.Core.Domain.Modules.Announcements;

namespace AnyTime.Core.Application.Contracts.Repositories;

public interface JobsRepository : GenericRepository<Job>
{
  Task<IReadOnlyList<Job>> GetAllByStatus(JobStatus status);
}