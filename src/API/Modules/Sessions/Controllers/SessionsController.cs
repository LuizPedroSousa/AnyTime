using AnyTime.API.Modules.Sessions.DTOs;
using AnyTime.API.Shared.Helpers.Mappers;
using AnyTime.Core.Application.Contracts.Providers.AutheticationProvider.DTOs;
using AnyTime.Core.Application.Features.Users.Commands.AuthenticateUser;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnyTime.API.Modules.Sessions.Controllers;

[ApiController]
[Route("/sessions")]
public class SessionsController : ControllerBase
{
  private readonly ILogger<SessionsController> _logger;
  private readonly IMediator _mediator;
  private readonly IMapper _mapper;

  public SessionsController(ILogger<SessionsController> logger, IMediator mediator, IMapper mapper)
  {
    _logger = logger;
    _mediator = mediator;
    _mapper = mapper;
  }


  [HttpPost()]
  public async Task<IActionResult> Create([FromBody] AuthenticateUserDTO dto)
  {
    var sessionOrError = await _mediator.Send(new AuthenticateUserCommand { dto = dto });

    if (sessionOrError.IsLeft())
    {
      return MapException.MapResult(sessionOrError.left);
    }

    return Created("/sessions", _mapper.Map<CreateSessionDTO>(sessionOrError.right));
  }
}
