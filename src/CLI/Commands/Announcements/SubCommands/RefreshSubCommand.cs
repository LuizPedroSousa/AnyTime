using AnyTime.CLI.Commands.Shared;
using AnyTime.Core.Application.Features.Announcements.Commands.UpdateAnnouncement;
using AnyTime.Core.Application.Features.Announcements.Queries.ListAnnouncements;
using AnyTime.Core.Application.Features.Announcements.Queries.ScrapAnnouncements;
using AnyTime.Core.Domain.Modules.Jobs;
using McMaster.Extensions.CommandLineUtils;
using MediatR;

namespace AnyTime.CLI.Commands.Announcements.SubCommands;

[Command("refresh")]
public class RefreshSubCommand : BaseCommand
{
  private readonly IMediator _mediator;

  public RefreshSubCommand(IMediator mediator)
  {
    _mediator = mediator;
  }

  protected async override Task<int> OnExecute(CommandLineApplication app)
  {
    var announcements = await _mediator.Send<IReadOnlyList<Announcement>>(new ListAnnouncementsQuery());


    foreach (var announcement in announcements)
    {
      var refreshedAnnouncementExists = await _mediator.Send(new ScrapFreelancerBrAnnouncementQuery { url = announcement.url });

      if (refreshedAnnouncementExists.IsLeft())
      {
        Console.WriteLine(refreshedAnnouncementExists.left.message);
        continue;
      }

      refreshedAnnouncementExists.right.id = announcement.id;

      await _mediator.Send<Unit>(new UpdateAnnouncementCommand { announcement = refreshedAnnouncementExists.right });
    }

    return await base.OnExecute(app);
  }
}
