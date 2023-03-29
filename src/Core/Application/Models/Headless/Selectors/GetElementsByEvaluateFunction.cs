namespace AnyTime.Core.Application.Models.Headless.Selectors;

public class GetElementsByEvaluateFunction : Selector
{
  public string script { get; set; }
  public object[] scriptParams { get; set; }
}
