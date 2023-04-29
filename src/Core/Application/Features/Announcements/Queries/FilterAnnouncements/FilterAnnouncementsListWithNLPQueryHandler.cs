using System.Text.RegularExpressions;
using AnyTime.Core.Application.Contracts.Providers.MarkdownProvider;
using AnyTime.Core.Application.Contracts.Providers.NLPProvider;
using AnyTime.Core.Domain.Modules.Announcements;
using MediatR;

namespace AnyTime.Core.Application.Features.Announcements.Queries.FilterAnnouncements;

public class FilterAnnouncementsListWithNLPQueryHandler : IRequestHandler<FilterAnnouncementsListWithNLPQuery, IReadOnlyList<Announcement>>
{

  private readonly MarkdownProvider _markdownProvider;

  private readonly NLPProvider _nlpProvider;

  public FilterAnnouncementsListWithNLPQueryHandler(MarkdownProvider markdownProvider, NLPProvider nlpProvider)
  {
    _markdownProvider = markdownProvider;
    _nlpProvider = nlpProvider;
  }

  public async Task<IReadOnlyList<Announcement>> Handle(FilterAnnouncementsListWithNLPQuery request, CancellationToken cancellationToken)
  {
    var curriculum = await this._markdownProvider.ParseToString("curriculum.md");

    var announcements = new List<Announcement>();

    foreach (Announcement announcement in request.announcements)
    {
      if (await ValidateAnnouncement(announcement, curriculum))
      {
        announcements.Add(announcement);
      }
      await Task.Delay(60000 * 3);
    }

    return announcements;
  }


  private async Task<bool> ValidateAnnouncement(Announcement announcement, string curriculum)
  {
    var question = @$"
Por favor com base no meu curriculo retorne um boleano onde true para quando for viavel aplicar e false para quando não for viavel aplicar
Quero um projeto rapido com no máximo 6 dias de duração.

 Este é meu curriculo:
 ""{curriculum}""
 Este é um anúncio de freelancing:
 ""{announcement.ToString()}""";

    var response = await this._nlpProvider.AskQuestion(question);

    if (Regex.IsMatch(response, @"False"))
    {
      return false;
    }

    return true;
  }
}
