using MediatR;

namespace AnyTime.Core.Application.Features.Proposals.Commands.WriteProposals;

using AnyTime.Core.Domain.Modules.Announcements;

public class WriteProposalsCommand : IRequest<Unit>
{
  public JobStatus status { get; set; }
}