using AnyTime.API.Modules.Announcements.Controllers.DTOs;
using AnyTime.API.Shared.Helpers.Mappers;
using AnyTime.Core.Application.Features.Announcements.Queries.GetAnnouncement;
using AnyTime.Core.Application.Features.Announcements.Queries.ListAnnouncements;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnyTime.API.Modules.Announcements.Controllers;

[ApiController]
[Route("/announcements")]
public class AnnouncementsController : ControllerBase
{
  private readonly ILogger<AnnouncementsController> _logger;
  private readonly IMediator _mediator;


  private readonly IMapper _mapper;

  public AnnouncementsController(ILogger<AnnouncementsController> logger, IMediator mediator, IMapper mapper)
  {
    _logger = logger;
    _mediator = mediator;
    _mapper = mapper;
  }

  [HttpGet()]
  public async Task<List<AnnouncementDTO>> GetAll()
  {
    var announcements = await _mediator.Send(new ListAnnouncementsQuery());

    return announcements.ToList().ConvertAll(element => _mapper.Map<AnnouncementDTO>(element));
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetById(string id)
  {
    var announcementOrError = await _mediator.Send(new GetAnnouncementByIdQuery { id = id });

    if (announcementOrError.IsLeft())
    {
      return MapException.MapResult(announcementOrError.left);
    }


    return Ok(_mapper.Map<AnnouncementDTO>(announcementOrError.right));
  }
}
