using AnyTime.Core.Application.Contracts.Providers;
using AnyTime.Core.Application.Models.Headless;
using AnyTime.Core.Domain.Modules.Jobs;
using MediatR;

namespace AnyTime.Application.Features.Announcements.Query.ScrapAnnouncements;

public class Scrap99FreelasAnnouncementsQueryHandler : IRequestHandler<Scrap99FreelasAnnouncementsQuery, List<Announcement>>
{
  private readonly HeadlessProvider _headlessProvider;
  public Scrap99FreelasAnnouncementsQueryHandler(HeadlessProvider headlessProvider)
  {
    _headlessProvider = headlessProvider;

  }

  public async Task<List<Announcement>> Handle(Scrap99FreelasAnnouncementsQuery request, CancellationToken cancellationToken)
  {

    await this._headlessProvider.open(
     new Open
     {
       headless = false
     });

    await this._headlessProvider.GoTo(new GoTo
    {
      url = "https://www.99freelas.com.br/projects?categoria=web-mobile-e-software&data-da-publicacao=menos-de-24-horas-atras",
    });

    return new List<Announcement>();
  }
}