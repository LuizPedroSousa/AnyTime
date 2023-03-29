using MarkdownMarkdig = Markdig;


namespace AnyTime.Infrastructure.Providers.Markdown;

using System.Threading.Tasks;
using AnyTime.Core.Application.Contracts.Providers.MarkdownProvider;
using AnyTime.Core.Application.Contracts.Providers.MarkdownProvider.Models;

public class MarkdigMarkdownProvider : MarkdownProvider
{
  public async Task<string> ParseToString(string path)
  {
    var markdown = await File.ReadAllTextAsync($"documents/{path}");

    var result = MarkdownMarkdig.Markdown.ToPlainText(markdown);

    return result;
  }

  public async Task Write(WriteModel model)
  {
    Directory.CreateDirectory($"documents/{model.folder}");
    await File.WriteAllTextAsync($"documents/{model.folder}/{model.filename}", model.content);
  }
}