using System.Text.Json;
using System.Text.Json.Serialization;
using AnyTime.API.Shared.Helpers.Mappers;
using AnyTime.Core.Application.Features.Announcements.Queries.GetAnnouncement;
using AnyTime.Core.Application.Features.Announcements.Queries.ListAnnouncements;
using AnyTime.Core.Domain.Modules.Jobs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnyTime.API.Modules.Announcements.Controllers;

[ApiController]
[Route("/announcements")]
public class AnnouncementsController : ControllerBase
{
  private readonly ILogger<AnnouncementsController> _logger;
  private readonly IMediator _mediator;

  public AnnouncementsController(ILogger<AnnouncementsController> logger, IMediator mediator)
  {
    _logger = logger;
    _mediator = mediator;
  }


  [HttpGet()]
  public async Task<IReadOnlyList<Announcement>> GetAll()
  {
    var announcements = await _mediator.Send(new ListAnnouncementsQuery());

    return announcements;
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetById(string id)
  {
    var announcementOrError = await _mediator.Send(new GetAnnouncementByIdQuery { id = id });

    if (announcementOrError.IsLeft())
    {
      return MapException.MapResult(announcementOrError.left);
    }


    return Ok(announcementOrError.right);
  }
}
