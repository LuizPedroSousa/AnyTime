using AnyTime.API.Shared.Helpers.Mappers;
using AnyTime.Core.Application.Contracts.Providers.AutheticationProvider.DTOs;
using AnyTime.Core.Application.Features.Users.Commands.CreateUser;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Responses = AnyTime.API.Modules.Users.DTOs;

namespace AnyTime.API.Modules.Users.Controllers;

[ApiController]
[Authorize]
[Route("/users")]
public class UsersController : ControllerBase
{
  private readonly ILogger<UsersController> _logger;
  private readonly IMediator _mediator;
  private readonly IMapper _mapper;

  public UsersController(ILogger<UsersController> logger, IMediator mediator, IMapper mapper)
  {
    _logger = logger;
    _mediator = mediator;
    _mapper = mapper;
  }

  [HttpPost()]
  [AllowAnonymous]
  public async Task<IActionResult> Create([FromBody] CreateUserDTO dto)
  {
    var userOrError = await _mediator.Send(new CreateUserCommand { dto = dto });

    if (userOrError.IsLeft())
    {
      return MapException.MapResult(userOrError.left);
    }

    return Created("/users", _mapper.Map<Responses.CreateUserDTO>(userOrError.right));
  }
}
