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


    var proposals = new List<Proposal>();
    var proposal_template = await this._markdownProvider.ParseToString($"proposals/templates/{request.proposal_template}");

    foreach (Announcement announcement in request.announcements)
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

      await Task.Delay(60000 * 3);
    }

    await this._proposalsRepository.CreateMany(proposals);

    return proposals;
  }
}