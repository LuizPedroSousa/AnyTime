namespace AnyTime.Core.Application.Contracts.Providers.HeadlessProvider.DTOs.Actions;

using AnyTime.Core.Application.Contracts.Providers.HeadlessProvider.DTOs.Actions.Shared;

public class FillInput : BaseAction
{
  public string content { get; set; }
}
