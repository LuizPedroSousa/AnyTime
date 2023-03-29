using System.Text.RegularExpressions;
using MediatR;

namespace AnyTime.Core.Application.Features.Proposals.Commands.CreateProposal;

using AnyTime.Core.Application.Contracts.Providers.MarkdownProvider;
using AnyTime.Core.Application.Contracts.Providers.NLPProvider;
using AnyTime.Core.Application.Contracts.Repositories;
using AnyTime.Core.Domain.Modules.Jobs;

public class CreateProposalsCommandHandler : IRequestHandler<CreateProposalsCommand, IReadOnlyList<Proposal>>
{
  private readonly NLPProvider _nLPProvider;
  private readonly MarkdownProvider _markdownProvider;
  private readonly ProposalsRepository _proposalsRepository;

  public CreateProposalsCommandHandler(NLPProvider nLPProvider,
                                       MarkdownProvider markdownProvider,
                                       ProposalsRepository proposalsRepository)
  {
    _nLPProvider = nLPProvider;
    _markdownProvider = markdownProvider;
    _proposalsRepository = proposalsRepository;
  }


  public async Task<IReadOnlyList<Proposal>> Handle(CreateProposalsCommand request, CancellationToken cancellationToken)
  {
    var curriculum = await this._markdownProvider.ParseToString("curriculum.md");

    var announcements = new List<Announcement>();

    foreach (Announcement announcement in request.announcements)
    {
      if (await ValidateAnnouncement(announcement, curriculum, request.platform))
      {
        announcements.Add(announcement);
      }
    }

    var proposals = new List<Proposal>();
    var proposal_template = await this._markdownProvider.ParseToString($"proposals/templates/{request.proposal_template}");

    foreach (Announcement announcement in announcements)
    {
      var proposal_description = await this._nLPProvider.AskQuestion(@$"
        Vou pedir que você preencha uma proposta porém na sua resposta quero que:
        - Apenas contenha o conteúdo da proposta
        - Preencha apenas onde estiver marcado com []

        anúncio: 
        ""{announcement.ToString()}""
        curriculo:
        ""{curriculum}""
        proposta: 
        ""{proposal_template}""
      ");

      proposals.Add(new Proposal(3000, proposal_description, announcement));
    }

    await this._proposalsRepository.CreateMany(proposals);

    return proposals;
  }

  private async Task<bool> ValidateAnnouncement(Announcement announcement, string curriculum, string platform)
  {

    var question = @$"
Por favor com base no meu curriculo retorne um boleano onde true para quando for viavel aplicar e false para quando não for viavel aplicar
Quero um projeto rapido com no máximo 6 dias de duração.

 Este é meu curriculo:
 ""{curriculum}""
 Este é uma anúncio de freelancing:
 ""{announcement}""";

    var response = await this._nLPProvider.AskQuestion(question);

    if (Regex.IsMatch(response, @"False"))
    {
      return false;
    }

    return true;
  }
}