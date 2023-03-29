namespace AnyTime.Core.Application.Contracts.Providers.HeadlessProvider;

using AnyTime.Core.Application.Models.Headless;
using AnyTime.Core.Application.Models.Headless.Selectors;

public interface HeadlessProvider
{
  Task Open(Open data);
  Task Close();
  Task GoTo(GoTo data);
  Task<bool> Click(Click data);
  Task<bool> FillInput(FillInput data);
  Task<bool> GetElement(GetElement data);
  Task<IEnumerable<T>> GetElementsByEvaluateFunction<T>(GetElementsByEvaluateFunction data) where T : class;
  Task<string?> GetText(GetText data);
  Task<string?> GetAttribute(GetAttribute data);
  Task Reload();
}
