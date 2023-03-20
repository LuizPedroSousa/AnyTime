using MediatR;

namespace AnyTime.Application.Features.Proposals.Commands.WriteProposals;

using AnyTime.Core.Domain.Modules.Jobs;

public class WriteProposalsCommand : IRequest<Unit>
{
  public List<Proposal> proposals { get; set; }
}