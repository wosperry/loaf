#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Loaf.Core.Data;

public interface IRepository<TEntity> where TEntity : class, IEntity
{
    public TEntity Insert(TEntity entity, bool autoSave = true);
    public Task<TEntity> InsertAsync(TEntity entity, bool autoSave = true, CancellationToken cancellationToken = default);
    public List<TEntity> Insert(IEnumerable<TEntity> entities, bool autoSave = true);
    public Task<List<TEntity>> InsertAsync(IEnumerable<TEntity> entities, bool autoSave = true, CancellationToken cancellationToken = default);
    public TEntity First(Expression<Func<TEntity, bool>> predicate);
    public Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    public TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
    public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    public List<TEntity> ToList(Expression<Func<TEntity, bool>> predicate);
    public Task<List<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    public TEntity Update(TEntity entity, bool autoSave = true);
    public Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = true, CancellationToken cancellationToken = default);
    public List<TEntity> Update(IEnumerable<TEntity> entities, bool autoSave = true);
    public Task<List<TEntity>> UpdateAsync(IEnumerable<TEntity> entities, bool autoSave = true, CancellationToken cancellationToken = default);
    public void Delete(TEntity entity, bool autoSave = true);
    public Task DeleteAsync(TEntity entity, bool autoSave = true, CancellationToken cancellationToken = default);
    public void SaveChanges();
    public Task SaveChangesAsync(CancellationToken cancellationToken);
    public Task<PagedResult<TEntity>> GetPagedResultAsync(Expression<Func<TEntity, bool>> predicate, IPagination pagination);
    public IQueryable<TEntity> GetQueryable();
    public IQueryable<TEntity> GetQueryable<TParameter>(TParameter parameter);
}
