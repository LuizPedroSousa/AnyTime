using AnyTime.Core.Application.Contracts.Providers.HeadlessProvider.DTOs.Actions;
using AnyTime.Core.Application.Contracts.Providers.HeadlessProvider.DTOs.Selectors;
using AnyTime.Core.Application.Models.Headless;
using AnyTime.Core.Domain.Shared;

namespace AnyTime.Core.Application.Contracts.Providers.HeadlessProvider;


public interface HeadlessProvider
{
  Task Open(Open data);
  Task Close();
  Task GoTo(GoTo data);
  Task Click(Click data);
  Task FillInput(FillInput data);
  Task Reload();

  Task<Either<BaseException, Element>> GetElement(GetElement data);
  Task<IEnumerable<T>> GetAllByFunctionEvaluation<T>(GetAllByFunctionEvaluation dto) where T : class;
  Task<Either<BaseException, string>> GetText(GetText data);
  Task<Either<BaseException, string>> GetAttribute(GetAttribute data);
  Task<bool> ElementExists(GetElement dto);
}
