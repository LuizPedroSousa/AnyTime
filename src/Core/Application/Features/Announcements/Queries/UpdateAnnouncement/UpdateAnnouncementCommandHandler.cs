using AnyTime.Core.Application.Contracts.Repositories;
using MediatR;

namespace AnyTime.Core.Application.Features.Announcements.Commands.UpdateAnnouncement;

public class UpdateAnnouncementCommandHandler : IRequestHandler<UpdateAnnouncementCommand, Unit>
{

  private readonly AnnouncementsRepository _announcementsRepository;

  public UpdateAnnouncementCommandHandler(AnnouncementsRepository announcementsRepository)
  {
    _announcementsRepository = announcementsRepository;
  }

  public async Task<Unit> Handle(UpdateAnnouncementCommand request, CancellationToken cancellationToken)
  {
    await _announcementsRepository.Update(request.announcement);

    return new Unit();
  }
}