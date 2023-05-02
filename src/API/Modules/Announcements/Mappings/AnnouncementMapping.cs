using AnyTime.API.Modules.Announcements.Controllers.DTOs;
using AnyTime.Core.Domain.Modules.Announcements;
using AutoMapper;

namespace AnyTime.API.Modules.Announcements.Mappings;

public class AnnouncementProfile : Profile
{
  public AnnouncementProfile()
  {
    CreateMap<Announcement, AnnouncementDTO>().ForMember(announcement => announcement.status, options =>
    {
      options.MapFrom(announcement => announcement.status == AnnouncementStatus.Open ? "open" : "closed");
    });

  }
}
