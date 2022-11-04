using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Loaf.EntityFrameworkCore.Core;

namespace Loaf.EntityFrameworkCore.Repository;

public class EfCoreRepository<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity
{
    public void Delete(TEntity entity, bool autoSave = true)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TEntity entity, bool autoSave = true, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public TEntity First(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public TEntity Insert(TEntity entity, bool autoSave = true)
    {
        throw new NotImplementedException();
    }

    public List<TEntity> Insert(IEnumerable<TEntity> entities, bool autoSave = true)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> InsertAsync(TEntity entity, bool autoSave = true, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<TEntity>> InsertAsync(IEnumerable<TEntity> entities, bool autoSave = true, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void SaveChanges()
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    public List<TEntity> ToList(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<List<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Update(TEntity entity, bool autoSave = true)
    {
        throw new NotImplementedException();
    }

    public TEntity Update(IEnumerable<TEntity> entities, bool autoSave = true)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = true, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> UpdateAsync(IEnumerable<TEntity> entities, bool autoSave = true, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}