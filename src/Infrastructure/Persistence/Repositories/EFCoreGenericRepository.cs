namespace AnyTime.Infrastructure.Persistence.Repositories;

using System.Collections.Generic;
using AnyTime.Core.Application.Contracts.Repositories;
using AnyTime.Core.Domain.Shared;
using AnyTime.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

public class EFCoreGenericRepository<E, T> : GenericRepository<E, T> where E : BaseException, new() where T : BaseEntity
{

  protected readonly PersistenceDatabaseContext _context;

  public EFCoreGenericRepository(PersistenceDatabaseContext context)
  {
    _context = context;
  }

  public async Task Create(T entity)
  {
    await _context.AddAsync(entity);
    await _context.SaveChangesAsync();
  }

  public async Task Delete(T entity)
  {
    _context.Remove(entity);
    await _context.SaveChangesAsync();
  }

  public async Task Update(T entity)
  {
    _context.Entry(entity).State = EntityState.Modified;
    await _context.SaveChangesAsync();
  }


  public async Task<IReadOnlyList<T>> Get()
  {
    return await this._context.Set<T>().AsNoTracking().ToListAsync();
  }

  public async Task<Either<E, T>> GetById(string id)
  {
    var entityExists = await this._context.Set<T>().AsNoTracking().FirstOrDefaultAsync(entity => entity.id == id);


    if (entityExists is null)
    {
      return new E();
    }


    return entityExists;
  }

  public async Task CreateMany(List<T> entities)
  {
    await this._context.AddRangeAsync(entities);
    await this._context.SaveChangesAsync();
  }
}
