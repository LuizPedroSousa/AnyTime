namespace AnyTime.Core.Application.Contracts.Repositories;

using AnyTime.Core.Domain.Modules.Jobs;
using AnyTime.Core.Domain.Modules.Jobs.Exceptions;

public interface ProposalsRepository : GenericRepository<ProposalNotFoundException, Proposal>
{
  Task<IReadOnlyList<Proposal>> GetWithAnnouncement();
}