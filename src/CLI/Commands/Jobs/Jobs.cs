using McMaster.Extensions.CommandLineUtils;
using MediatR;

namespace AnyTime.CLI.Commands.Jobs;

using System.ComponentModel.DataAnnotations;
using AnyTime.CLI.Commands.Jobs.SubCommands;
using AnyTime.CLI.Commands.Shared;
using AnyTime.Core.Application.Features.Announcements.Queries.FilterAnnouncements;
using AnyTime.Core.Application.Features.Announcements.Queries.ScrapAnnouncements;
using AnyTime.Core.Application.Features.Jobs.Commands.CreateJob;
using AnyTime.Core.Application.Features.Platforms.Queries;
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


  [Required(ErrorMessage = "Platform name must be provided")]
  [Option("-p|--platform", Description = "Platform name")]
  public string platform_name { get; set; }

  protected async override Task<int> OnExecute(CommandLineApplication app)
  {

    var platformExists = await _mediator.Send(new GetPlatformByNameQuery { name = platform_name });

    if (platformExists.IsLeft())
    {
      Console.WriteLine(platformExists.left.message);
      return await base.OnExecute(app);
    }

    var announcements = await _mediator.Send<IReadOnlyList<Announcement>>(new ScrapFreelancerBrAnnouncementsQuery { headless = false, platform = platformExists.right });

    var filteredAnnouncements = await _mediator.Send<IReadOnlyList<Announcement>>(new FilterAnnouncementsListWithNLPQuery { announcements = announcements });

    var proposals = await _mediator.Send<IReadOnlyList<Proposal>>(new CreateProposalsCommand { announcements = filteredAnnouncements, proposal_template = "CommonProposal.md" });

    await _mediator.Send<Unit>(new CreateJobsByProposalsCommand { proposals = proposals });

    Console.WriteLine($"From {announcements.Count} scrapped ${filteredAnnouncements.Count} are valid announcements");

    return await base.OnExecute(app);
  }
}
