namespace AnyTime.Core.Application.Contracts.Providers.NLPProvider;

public interface NLPProvider
{
  Task<string> AskQuestion(string question);
}