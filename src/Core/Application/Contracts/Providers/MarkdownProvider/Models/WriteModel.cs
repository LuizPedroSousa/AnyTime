namespace AnyTime.Core.Application.Contracts.Providers.MarkdownProvider.Models;

public class WriteModel
{
  public string filename { get; set; }
  public string folder { get; set; }

  public string content { get; set; }
}