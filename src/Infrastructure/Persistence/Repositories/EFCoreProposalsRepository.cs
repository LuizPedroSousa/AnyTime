using Microsoft.EntityFrameworkCore;

namespace AnyTime.Infrastructure.Persistence.Repositories;

using AnyTime.Core.Application.Contracts.Repositories;
using AnyTime.Core.Domain.Modules.Proposals;
using AnyTime.Core.Domain.Shared;
using AnyTime.Infrastructure.Persistence.DatabaseContext;

public class EFCoreProposalsRepository : EFCoreGenericRepository<Proposal>, ProposalsRepository
{
  public EFCoreProposalsRepository(PersistenceDatabaseContext context) : base(context, new NotFoundException("Proposals not found"))
  {
  }

  public async Task<IReadOnlyList<Proposal>> GetWithAnnouncement()
  {
    return await _context.proposals.Include(proposal => proposal.announcement).ToListAsync();
  }
}