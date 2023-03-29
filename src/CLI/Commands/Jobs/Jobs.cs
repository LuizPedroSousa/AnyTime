using System.Reflection;
using McMaster.Extensions.CommandLineUtils;
using MediatR;

namespace AnyTime.CLI.Commands.Jobs;

using AnyTime.CLI.Commands.Jobs.SubCommands;
using AnyTime.CLI.Commands.Shared;
using AnyTime.Core.Application.Features.Announcements.Queries.ScrapAnnouncements;
using AnyTime.Core.Application.Features.Jobs.Commands.CreateJob;
using AnyTime.Core.Application.Features.Proposals.Commands.CreateProposal;
using AnyTime.Core.Domain.Modules.Jobs;

[Command("jobs")]
[Subcommand(typeof(PullSubCommand))]
public class JobsCommand : BaseCommand
{
  private readonly IMediator _mediator;
  public JobsCommand(IMediator mediator)
  {
    _mediator = mediator;
  }

  private static string GetVersion() => typeof(AnyTimeCommand).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;





  protected async override Task<int> OnExecute(CommandLineApplication app)
  {
    var announcements = await _mediator.Send<List<Announcement>>(new Scrap99FreelasAnnouncementsQuery { headless = true });

    var proposals = await _mediator.Send<IReadOnlyList<Proposal>>(new CreateProposalsCommand { announcements = announcements, platform = "99freelas", proposal_template = "CommonProposal.md" });

    await _mediator.Send<Unit>(new CreateJobsByProposalsCommand { proposals = proposals });

    Console.WriteLine($"From {announcements.Count} announcements you can apply to ${proposals.Count}");

    return await base.OnExecute(app);
  }


}
