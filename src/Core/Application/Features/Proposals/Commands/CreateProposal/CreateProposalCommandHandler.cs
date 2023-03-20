using MediatR;

namespace AnyTime.Application.Features.Proposals.Commands.CreateProposal;

using System.Text.RegularExpressions;
using AnyTime.Core.Application.Contracts.Providers.MarkdownProvider;
using AnyTime.Core.Application.Contracts.Providers.NLPProvider;
using AnyTime.Core.Domain.Modules.Jobs;

public class CreateProposalsCommandHandler : IRequestHandler<CreateProposalsCommand, List<Proposal>>
{
  private readonly NLPProvider _nLPProvider;
  private readonly MarkdownProvider _markdownProvider;

  public CreateProposalsCommandHandler(NLPProvider nLPProvider, MarkdownProvider markdownProvider
  )
  {
    _nLPProvider = nLPProvider;
    _markdownProvider = markdownProvider;
  }

  public async Task<List<Proposal>> Handle(CreateProposalsCommand request, CancellationToken cancellationToken)
  {
    var curriculum = await this._markdownProvider.ParseToString("curriculum.md");

    var question = @$"
 Este é meu curriculo:
 ""{curriculum}""
 Estes são anúncios do {request.platform}:
 ""{Announcement.ToString(request.announcements)}""
 Por favor com base no meu curriculo retorne uma array de ids dos anúncios que dão match de acordo com minhas habilidades.";

    var ids = await this._nLPProvider.AskQuestion(question);

    var announcements = new List<Announcement>();

    foreach (string id in GetUuidsFromString(ids))
    {
      Console.WriteLine(id);
      announcements.AddRange(request.announcements.Where(announcement => announcement.id == id));
    }

    var proposal_template = await this._markdownProvider.ParseToString($"proposals/templates/{request.proposal_template}");

    var proposals = new List<Proposal>();

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

      Console.WriteLine($"{proposal_description}");

      proposals.Add(new Proposal(3000, proposal_description, announcement));
    }

    return proposals;
  }


  private List<string> GetUuidsFromString(string input)
  {
    // Define the pattern to match UUIDs
    string pattern = @"\b[a-f\d]{8}(-[a-f\d]{4}){3}-[a-f\d]{12}\b";

    // Create a list to hold the UUIDs
    List<string> uuids = new List<string>();

    // Find all matches of the pattern in the input string
    MatchCollection matches = Regex.Matches(input, pattern);

    // Loop through the matches and add them to the list
    foreach (Match match in matches)
    {
      uuids.Add(match.Value);
    }

    // Return the list of UUIDs
    return uuids;
  }
}