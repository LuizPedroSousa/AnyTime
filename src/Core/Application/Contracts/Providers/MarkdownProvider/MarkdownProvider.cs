namespace AnyTime.Core.Application.Contracts.Providers.MarkdownProvider;

using AnyTime.Core.Application.Contracts.Providers.MarkdownProvider.Models;

public interface MarkdownProvider
{
  Task<string> ParseToString(string markdown_path);
  Task Write(WriteModel model);
}