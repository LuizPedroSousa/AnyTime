using AnyTime.CLI.Commands.Shared;
using AnyTime.Core.Application.Features.Proposals.Commands.WriteProposals;
using AnyTime.Core.Application.Features.Proposals.Queries.ListProposals;
using AnyTime.Core.Domain.Modules.Jobs;
using McMaster.Extensions.CommandLineUtils;
using MediatR;

namespace AnyTime.CLI.Commands.Jobs.SubCommands;


[Command("pull")]
public class PullSubCommand : BaseCommand
{
  private readonly IMediator _mediator;
  public PullSubCommand(IMediator mediator)
  {
    _mediator = mediator;
  }

  protected async override Task<int> OnExecute(CommandLineApplication app)
  {
    var proposals = await _mediator.Send<IReadOnlyList<Proposal>>(new ListProposalsQuery());

    await _mediator.Send<Unit>(new WriteProposalsCommand { status = JobStatus.Rejected });
    await _mediator.Send<Unit>(new WriteProposalsCommand { status = JobStatus.Pending });
    await _mediator.Send<Unit>(new WriteProposalsCommand { status = JobStatus.Sended });
    await _mediator.Send<Unit>(new WriteProposalsCommand { status = JobStatus.Accepted });

    return await base.OnExecute(app);
  }


}
