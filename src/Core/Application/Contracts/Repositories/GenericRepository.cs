using AnyTime.Core.Domain.Shared;

namespace AnyTime.Core.Application.Contracts.Repositories;

public interface GenericRepository<E, T> where E : BaseException, new() where T : BaseEntity
{
  Task<IReadOnlyList<T>> Get();
  Task<Either<E, T>> GetById(string id);
  Task Create(T entity);
  Task CreateMany(List<T> entity);
  Task Delete(T entity);
  Task Update(T entity);
}