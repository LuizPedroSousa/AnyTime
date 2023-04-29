using AnyTime.Core.Domain.Shared;

namespace AnyTime.Core.Application.Contracts.Repositories;

public interface GenericRepository<T> where T : BaseEntity
{
  Task<IReadOnlyList<T>> Get();
  Task<Either<NotFoundException, T>> GetById(string id);
  Task Create(T entity);
  Task CreateMany(List<T> entity);
  Task Delete(T entity);
  Task Update(T entity);
}