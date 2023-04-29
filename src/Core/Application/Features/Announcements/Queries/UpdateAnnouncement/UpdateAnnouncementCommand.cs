using AnyTime.Core.Domain.Modules.Jobs;
using MediatR;

namespace AnyTime.Core.Application.Features.Announcements.Commands.UpdateAnnouncement;

public class UpdateAnnouncementCommand : IRequest<Unit>
{
  public Announcement announcement { get; set; }
}