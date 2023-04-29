using MediatR;

namespace AnyTime.Core.Application.Features.Announcements.Commands.CreateJob;

using AnyTime.Core.Domain.Modules.Proposals;

public class CreateJobsByProposalsCommand : IRequest<Unit>
{
  public IReadOnlyList<Proposal> proposals { get; set; }
}