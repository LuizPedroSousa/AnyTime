using MediatR;

namespace AnyTime.Core.Application.Features.Announcements.Queries.ListAnnouncements;

using AnyTime.Core.Application.Contracts.Repositories;
using AnyTime.Core.Domain.Modules.Jobs;

public class ListAnnouncementsQueryHandler : IRequestHandler<ListAnnouncementsQuery, IReadOnlyList<Announcement>>
{
  private readonly AnnouncementsRepository _announcementsRepository;
  public ListAnnouncementsQueryHandler(AnnouncementsRepository announcementsRepository)
  {
    _announcementsRepository = announcementsRepository;
  }

  public async Task<IReadOnlyList<Announcement>> Handle(ListAnnouncementsQuery request, CancellationToken cancellationToken)
  {
    return await _announcementsRepository.Get();
  }
}
