using AnyTime.Core.Application.Contracts.Repositories;
using AnyTime.Core.Domain.Modules.Jobs;
using AnyTime.Core.Domain.Shared;
using MediatR;

namespace AnyTime.Core.Application.Features.Announcements.Queries.GetAnnouncement;

public class GetAnnouncementByIdQueryHandler : IRequestHandler<GetAnnouncementByIdQuery, Either<NotFoundException, Announcement>>
{

  private readonly AnnouncementsRepository _announcementsRepository;

  public GetAnnouncementByIdQueryHandler(AnnouncementsRepository announcementsRepository)
  {
    _announcementsRepository = announcementsRepository;
  }

  public async Task<Either<NotFoundException, Announcement>> Handle(GetAnnouncementByIdQuery request, CancellationToken cancellationToken)
  {
    var announcementExists = await _announcementsRepository.GetByIdFull(request.id);

    if (announcementExists.IsLeft())
    {
      return announcementExists.left;
    }

    return announcementExists.right;
  }
}
