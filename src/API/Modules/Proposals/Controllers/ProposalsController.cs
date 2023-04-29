using AnyTime.API.Shared.Helpers.Mappers;
using AnyTime.Core.Application.Features.Proposals.Commands.UpdateProposal;
using AnyTime.Core.Application.Features.Proposals.Commands.UpdateProposal.Responses;
using AnyTime.Core.Application.Features.Proposals.Queries.ListProposals;
using AnyTime.Core.Domain.Modules.Proposals;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnyTime.API.Modules.Proposals.Controllers;

[ApiController]
[Route("/proposals")]
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
  public async Task<IReadOnlyList<Proposal>> GetAll()
  {
    var proposals = await _mediator.Send(new ListProposalsQuery());

    return proposals;
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateById([FromRoute] string id, [FromBody] UpdateProposalCommand command)
  {
    var proposalOrError = await _mediator.Send<UpdateProposalResponse>(new UpdateProposalCommand
    {
      id = id,
      description = command.description,
      title = command.title
    });

    if (proposalOrError.IsLeft())
    {

      return MapException.MapResult(proposalOrError.left);
    }

    return Ok(proposalOrError.right);
  }
}
