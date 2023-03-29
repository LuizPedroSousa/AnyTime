using MediatR;

namespace AnyTime.Core.Application.Features.Proposals.Queries.ListProposals;

using AnyTime.Core.Application.Contracts.Repositories;
using AnyTime.Core.Domain.Modules.Jobs;

public class ListProposalsQueryHandler : IRequestHandler<ListProposalsQuery, IReadOnlyList<Proposal>>
{
  private readonly ProposalsRepository _proposalsRepository;
  public ListProposalsQueryHandler(ProposalsRepository proposalsRepository)
  {
    _proposalsRepository = proposalsRepository;
  }

  public async Task<IReadOnlyList<Proposal>> Handle(ListProposalsQuery request, CancellationToken cancellationToken)
  {
    return await this._proposalsRepository.GetWithAnnouncement();
  }
}