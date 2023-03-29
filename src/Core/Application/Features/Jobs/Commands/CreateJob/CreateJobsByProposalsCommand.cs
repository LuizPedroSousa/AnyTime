using MediatR;

namespace AnyTime.Core.Application.Features.Jobs.Commands.CreateJob;

using AnyTime.Core.Domain.Modules.Jobs;

public class CreateJobsByProposalsCommand : IRequest<Unit>
{
  public IReadOnlyList<Proposal> proposals { get; set; }
}