using MediatR;

namespace AnyTime.Core.Application.Features.Proposals.Commands.CreateProposal;

using AnyTime.Core.Domain.Modules.Jobs;

public class CreateProposalsCommand : IRequest<IReadOnlyList<Proposal>>
{
  public string proposal_template { get; set; }
  public IReadOnlyList<Announcement> announcements { get; set; }
}