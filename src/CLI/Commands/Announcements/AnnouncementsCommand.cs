using System.Reflection;
using McMaster.Extensions.CommandLineUtils;
using MediatR;

namespace AnyTime.CLI.Commands.Announcements;

using AnyTime.CLI.Commands.Announcements.SubCommands;
using AnyTime.CLI.Commands.Shared;

[Command("announcements")]
[Subcommand(typeof(RefreshSubCommand))]
public class AnnouncementsCommand : BaseCommand
{
  private readonly IMediator _mediator;
  public AnnouncementsCommand(IMediator mediator)
  {
    _mediator = mediator;
  }

  private static string GetVersion() => typeof(AnyTimeCommand).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

  protected async override Task<int> OnExecute(CommandLineApplication app)
  {

    return await base.OnExecute(app);
  }
}
