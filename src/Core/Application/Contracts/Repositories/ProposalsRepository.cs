namespace AnyTime.Core.Application.Contracts.Repositories;

using AnyTime.Core.Domain.Modules.Proposals;

public interface ProposalsRepository : GenericRepository<Proposal>
{
  Task<IReadOnlyList<Proposal>> GetWithAnnouncement();
}