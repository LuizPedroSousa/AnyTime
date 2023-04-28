namespace AnyTime.Core.Application.Contracts.Providers.HeadlessProvider.DTOs.Actions.Shared;

public class BaseAction
{
  public string target { get; set; }
  public int? timeout { get; set; }
  public int maxSelectorTimeout { get; set; }
  public bool isIframe { get; set; }
  public bool logError { get; set; } = true;
  public ElementChecker? checker { get; set; }
}