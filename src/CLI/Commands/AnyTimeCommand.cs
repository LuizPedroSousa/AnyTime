using System.Reflection;
using AnyTime.CLI.Commands.Announcements;
using AnyTime.CLI.Commands.Shared;
using McMaster.Extensions.CommandLineUtils;

namespace AnyTime.CLI.Commands;

[Command("AnyTime")]
[Subcommand(typeof(JobsCommand))]
[Subcommand(typeof(AnnouncementsCommand))]
public class AnyTimeCommand : BaseCommand
{

  [Option("-v", Description = "Show current version of AnyTime")]
  public bool version { get; set; } = false;

  private static string GetVersion() => typeof(AnyTimeCommand).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

  protected async override Task<int> OnExecute(CommandLineApplication app)
  {

    if (version)
    {
      Console.WriteLine(GetVersion());
    }


    return await base.OnExecute(app);
  }

}
