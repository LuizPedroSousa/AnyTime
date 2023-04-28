namespace AnyTime.Core.Application.Contracts.Providers.HeadlessProvider.DTOs.Selectors;

using AnyTime.Core.Application.Contracts.Providers.HeadlessProvider.DTOs.Selectors.Shared;

public class GetAllByFunctionEvaluation : BaseSelector
{
  public string script { get; set; }
  public object[] scriptParams { get; set; }
}
