using System.Reflection;
using AnyTime.Application.Features.Announcements.Query.ScrapAnnouncements;
using AnyTime.Application.Features.Proposals.Commands.CreateProposal;
using AnyTime.Application.Features.Proposals.Commands.WriteProposals;
using AnyTime.CLI.Commands.Shared;
using AnyTime.Core.Domain.Modules.Jobs;
using McMaster.Extensions.CommandLineUtils;
using MediatR;

namespace AnyTime.CLI.Commands.Jobs;

[Command("jobs")]
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


    // var proposals = await _mediator.Send<List<Proposal>>(new CreateProposalsCommand() { announcements = announcements, platform = "99freelas", proposal_template = "CommonProposal.md" });

    // await _mediator.Send<Unit>(new WriteProposalsCommand { proposals = proposals });


    return await base.OnExecute(app);
  }


}
