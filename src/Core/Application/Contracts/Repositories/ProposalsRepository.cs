namespace AnyTime.Core.Application.Contracts.Repositories;

using AnyTime.Core.Domain.Modules.Jobs;

public interface ProposalsRepository : GenericRepository<Proposal>
{
  Task<IReadOnlyList<Proposal>> GetWithAnnouncement();
}