using Flurl.Http;
using Microsoft.Extensions.Configuration;

namespace AnyTime.Infrastructure.Providers.NLP;

using AnyTime.Core.Application.Contracts.Providers.NLPProvider;

public struct ChoiceMessage
{
  public string content { get; set; }

}
public struct Choice
{
  public ChoiceMessage message { get; set; }
}

public class Response
{
  public Choice[] choices { get; set; }
}

public class GPTNLPProvider : NLPProvider
{

  private readonly HttpClient _httpClient;

  private readonly string _base_url;
  private readonly IConfiguration _configuration;

  public GPTNLPProvider(IConfiguration configuration)
  {
    _base_url = "https://api.openai.com/v1";
    _configuration = configuration;
  }

  public async Task<string> AskQuestion(string question)
  {
    try
    {
      var response = await $"{_base_url}/chat/completions"
        .WithOAuthBearerToken(_configuration.GetValue<string>("OPEN_API_KEY"))
        .PostJsonAsync(new
        {
          model = "gpt-3.5-turbo",
          messages = new[] {
            new {
              role = "user",
              content = question
            }
          },
        }).ReceiveJson<Response>();

      return response.choices[0].message.content;
    }
    catch (FlurlHttpException exception)
    {
      Console.WriteLine(await exception.GetResponseStringAsync());
      return "";
    }
  }
}