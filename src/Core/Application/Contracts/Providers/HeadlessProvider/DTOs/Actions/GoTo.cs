namespace AnyTime.Core.Application.Contracts.Providers.HeadlessProvider.DTOs.Actions;

using AnyTime.Core.Application.Contracts.Providers.HeadlessProvider.DTOs.Actions.Shared;

public class GoTo : BaseAction
{
  public string url { get; set; }
  public int navigation_timeout { get; set; }
  public int? timeout { get; set; }
}
