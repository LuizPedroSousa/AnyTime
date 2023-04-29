using MediatR;

namespace AnyTime.Core.Application.Features.Announcements.Queries.ScrapAnnouncements;

using AnyTime.Core.Application.Contracts.Providers.HeadlessProvider;
using AnyTime.Core.Application.Contracts.Providers.HeadlessProvider.DTOs.Actions;
using AnyTime.Core.Application.Contracts.Providers.HeadlessProvider.DTOs.Selectors;
using AnyTime.Core.Application.Contracts.Repositories;
using AnyTime.Core.Domain.Modules.Announcements;
using AnyTime.Core.Domain.Modules.Platforms;

public class Scrap99FreelasAnnouncementsQueryHandler : IRequestHandler<Scrap99FreelasAnnouncementsQuery, List<Announcement>>
{
  private readonly HeadlessProvider _headlessProvider;
  private readonly AnnouncementsRepository _announcementsRepository;
  private readonly string base_url;
  public Scrap99FreelasAnnouncementsQueryHandler(AnnouncementsRepository announcementsRepository, HeadlessProvider headlessProvider)
  {
    _headlessProvider = headlessProvider;
    _announcementsRepository = announcementsRepository;
    base_url = "https://www.99freelas.com.br";
  }

  public async Task<List<Announcement>> Handle(Scrap99FreelasAnnouncementsQuery request, CancellationToken cancellationToken)
  {
    await this._headlessProvider.Open(
     new Open
     {
       headless = false
     });

    var existentLinks = await _announcementsRepository.GetUrlsByPlatform(request.platform);

    var links = new List<string>();

    for (int i = 0; i < 3; i++)
    {
      links.AddRange(await GetAnnoucementsUrls(existentLinks: existentLinks.Union(links).ToList(), queryParams: $"&page={i}"));
    }

    var announcements = await GetAnnouncementsByLinks(request.platform, links);

    await this._headlessProvider.Close();

    return announcements;
  }

  private async Task<List<string>> GetAnnoucementsUrls(IReadOnlyList<string> existentLinks, string? queryParams = "")
  {
    await this._headlessProvider.GoTo(new GoTo
    {
      url = $"{base_url}/projects?categoria=web-mobile-e-software&data-da-publicacao=menos-de-24-horas-atras{queryParams}",
    });

    var container = "body > div.page-simple-wrapper > div.page-simple-content.box-projects > div.projects-result > ul > li";

    var links = await this._headlessProvider.GetAllByFunctionEvaluation<string>(new GetAllByFunctionEvaluation
    {
      target = container,
      script = @"(container) => {
        const announcements = [];
        const items = document.querySelectorAll(container) || [];

        items.forEach(item => {
          const anchor = item.querySelector(""hgroup > h1 > a"");
          announcements.push(
            `https://www.99freelas.com.br${anchor.getAttribute(""href"")}`
          );
        });

        return Array.from(announcements)
      }",
      scriptParams = new[] { container },
    });

    var filteredLinks = links.Where(link => !existentLinks.Contains(link))
      .Distinct()
      .ToList();

    return filteredLinks;
  }


  private async Task<List<Announcement>> GetAnnouncementsByLinks(Platform platform, List<string> links)
  {
    var container = "body > div.page-simple-wrapper > div.page-simple-content > div.box-project-view > div.box-project-view-top > div > div.box-project-view-principal.with-flag";

    var announcements = new List<Announcement>();

    foreach (var link in links)
    {
      await this._headlessProvider.GoTo(new GoTo { url = link });

      var titleExists = await this._headlessProvider.GetText(new GetText
      {
        target = $"{container} > h1 > span.nomeProjeto",
        maxSelectorTimeout = 2000
      });

      var descriptionExists = await this._headlessProvider.GetText(new GetText
      {
        target = $"{container} > div:nth-child(4) > div",
        maxSelectorTimeout = 2000
      });

      if (descriptionExists.IsLeft())
      {
        descriptionExists = await this._headlessProvider.GetText(new GetText
        {
          target = $"{container} > div:nth-child(5) > div",
          maxSelectorTimeout = 2000
        });
      }

      if (titleExists.IsLeft() && descriptionExists.IsLeft())
      {
        continue;
      }

      announcements.Add(new Announcement(titleExists.right,
                                         descriptionExists.right,
                                         new List<string>(),
                                         link,
                                         new Author("-", "-"),
                                         platform));
    };

    return announcements;
  }
}