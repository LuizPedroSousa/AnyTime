namespace AnyTime.Core.Application.Models.Headless;

public class GoTo
{
  public string url { get; set; }
  public int navigation_timeout { get; set; }
  public int? timeout { get; set; }
}
