using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AnyTime.Infrastructure.Persistence.Repositories;

using AnyTime.Core.Application.Contracts.Repositories;
using AnyTime.Core.Domain.Modules.Jobs;
using AnyTime.Core.Domain.Modules.Jobs.Exceptions;
using AnyTime.Infrastructure.Persistence.DatabaseContext;

public class EFCoreProposalsRepository : EFCoreGenericRepository<ProposalNotFoundException, Proposal>, ProposalsRepository
{
  public EFCoreProposalsRepository(PersistenceDatabaseContext context) : base(context)
  {
  }

  public async Task<IReadOnlyList<Proposal>> GetWithAnnouncement()
  {
    return await _context.proposals.Include(proposal => proposal.announcement).ToListAsync();
  }
}