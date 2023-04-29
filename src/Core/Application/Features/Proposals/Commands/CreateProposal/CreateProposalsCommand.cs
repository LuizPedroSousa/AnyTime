using MediatR;

namespace AnyTime.Core.Application.Features.Proposals.Commands.CreateProposal;

using AnyTime.Core.Domain.Modules.Announcements;
using AnyTime.Core.Domain.Modules.Proposals;

public class CreateProposalsCommand : IRequest<IReadOnlyList<Proposal>>
{
  public string proposal_template { get; set; }
  public IReadOnlyList<Announcement> announcements { get; set; }
}