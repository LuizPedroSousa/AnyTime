using MediatR;

namespace AnyTime.Core.Application.Features.Announcements.Queries.ScrapAnnouncements;

using AnyTime.Core.Application.Contracts.Providers.HeadlessProvider;
using AnyTime.Core.Application.Contracts.Providers.HeadlessProvider.DTOs.Actions;
using AnyTime.Core.Application.Contracts.Providers.HeadlessProvider.DTOs.Selectors;
using AnyTime.Core.Application.Contracts.Repositories;
using AnyTime.Core.Domain.Modules.Jobs;
using AnyTime.Core.Domain.Modules.Platforms;

public class ScrapFreelancerBrAnnouncementsQueryHandler : IRequestHandler<ScrapFreelancerBrAnnouncementsQuery, IReadOnlyList<Announcement>>
{
  private readonly HeadlessProvider _headlessProvider;
  private readonly AnnouncementsRepository _announcementsRepository;

  private readonly IMediator _mediator;

  private readonly string base_url;

  public ScrapFreelancerBrAnnouncementsQueryHandler(HeadlessProvider headlessProvider, AnnouncementsRepository announcementsRepository, IMediator mediator)
  {
    _headlessProvider = headlessProvider;
    _announcementsRepository = announcementsRepository;
    base_url = "https://freelancer.com.br";
    _mediator = mediator;
  }



  public async Task<IReadOnlyList<Announcement>> Handle(ScrapFreelancerBrAnnouncementsQuery request, CancellationToken cancellationToken)
  {
    await _headlessProvider.Open(new Open { headless = request.headless });

    var existentLinks = await _announcementsRepository.GetUrlsByPlatform(request.platform);

    var links = new List<string>();

    for (int i = 0; i < 5; i++)
    {
      links.AddRange(await GetAnnoucementsUrls(existentLinks: existentLinks.Union(links).ToList(), queryParams: $"&page={i}"));
    }

    await this._headlessProvider.Close();

    var announcements = await GetAnnouncementsByLinks(request.platform, links);

    return announcements;

  }

  private async Task<List<string>> GetAnnoucementsUrls(IReadOnlyList<string> existentLinks, string? queryParams = "")
  {
    await this._headlessProvider.GoTo(new GoTo
    {
      url = $"{base_url}/projetos?category=3&skills=&budgetType=&budgetRange=0-10001&hourlyRate=20-101&distance=50&status=statusOpenProjects&maxInterested=11&currentLocation=&locGeoId=PRO%2FGEO%2FREM{queryParams}",
    });

    var container = "#generalApp > main > div > div.container > div.assignments-cards-wrap > div";

    var links = await this._headlessProvider.GetAllByFunctionEvaluation<string>(new GetAllByFunctionEvaluation
    {
      target = container,
      script = @"(container) => {
        const announcements = [];
        const items = document.querySelectorAll(container) || [];

        items.forEach(item => {
          const anchor = item.querySelector(""div:nth-child(2) > div.content.main-text.bold > a"");
          announcements.push(
           anchor.getAttribute(""href"")
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


  private async Task<List<Announcement>> GetAnnouncementsByLinks(Platform platform, List<string> urls)
  {
    var announcements = new List<Announcement>();

    foreach (var url in urls)
    {
      var announcementExists = await _mediator.Send(new ScrapFreelancerBrAnnouncementQuery { url = url, platform = platform });

      if (announcementExists.IsLeft())
      {
        continue;
      }

      announcements.Add(announcementExists.right);
    };

    return announcements;
  }
}