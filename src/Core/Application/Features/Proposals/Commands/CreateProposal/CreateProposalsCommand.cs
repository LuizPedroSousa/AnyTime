using MediatR;

namespace AnyTime.Core.Application.Features.Proposals.Commands.CreateProposal;

using AnyTime.Core.Domain.Modules.Jobs;

public class CreateProposalsCommand : IRequest<IReadOnlyList<Proposal>>
{
  public string platform { get; set; }
  public string proposal_template { get; set; }
  public List<Announcement> announcements { get; set; }
}