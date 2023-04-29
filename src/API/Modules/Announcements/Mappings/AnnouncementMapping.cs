using AnyTime.API.Modules.Announcements.Controllers.DTOs;
using AnyTime.Core.Domain.Modules.Announcements;
using AutoMapper;

namespace AnyTime.Core.Application.Features.Proposals.Commands.UpdateProposal.Mappings;

public class ProposalProfile : Profile
{
  public ProposalProfile()
  {
    CreateMap<Announcement, AnnouncementDTO>().ForMember(announcement => announcement.status, options =>
    {
      options.MapFrom(announcement => announcement.status == AnnouncementStatus.Open ? "open" : "closed");
    });

  }
}
