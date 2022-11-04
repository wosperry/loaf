#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Loaf.EntityFrameworkCore.Core;
using Loaf.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Loaf.EntityFrameworkCore.Repository;

public class EfCoreRepository<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity
{
    private readonly LoafDbContext _dbContext;
    private DbSet<TEntity> DbSet => _dbContext.Set<TEntity>();

    private async Task SaveChangeIfAutoSaveAsync(bool autoSave, CancellationToken cancellationToken = default)
    {
        if (autoSave)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
    private void SaveChangeIfAutoSave(bool autoSave)
    {
        if (autoSave)
        {
             _dbContext.SaveChanges();
        }
    }

    public EfCoreRepository(ILoafDbContextFinder<TEntity> finder)
    {
        _dbContext = finder.GetDb();
    }

    public void Delete(TEntity entity, bool autoSave = false)
    {
        DbSet.Remove(entity);
        SaveChangeIfAutoSave(autoSave);
    }

    public async Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
    {
       
        DbSet.Remove(entity);
        await SaveChangeIfAutoSaveAsync(autoSave, cancellationToken);
    }

    public TEntity First(Expression<Func<TEntity, bool>> predicate) => DbSet.First(predicate);

    public Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
        => DbSet.FirstAsync(predicate, cancellationToken);

    public TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate) => DbSet.FirstOrDefault();

    public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default) => DbSet.FirstOrDefaultAsync(predicate, cancellationToken);

    public TEntity Insert(TEntity entity, bool autoSave = false)
    {
        DbSet.Add(entity);
        SaveChangeIfAutoSave(autoSave);
        return entity;
    }

    public List<TEntity> Insert(IEnumerable<TEntity> entities, bool autoSave = false)
    {
        var data = entities as List<TEntity> ?? entities.ToList();
        DbSet.AddRange(data);
        SaveChangeIfAutoSave(autoSave);
        return data;
    }

    public async Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false,
        CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
        await SaveChangeIfAutoSaveAsync(autoSave,cancellationToken);
        return entity;
    }

    public async Task<List<TEntity>> InsertAsync(IEnumerable<TEntity> entities, bool autoSave = false,
        CancellationToken cancellationToken = default)
    {
        var data = entities as List<TEntity> ?? entities.ToList();
        await DbSet.AddRangeAsync(data, cancellationToken);
        await SaveChangeIfAutoSaveAsync(autoSave, cancellationToken);
        return data;
    }

    public void SaveChanges() => SaveChangeIfAutoSave(true);

    public Task SaveChangesAsync(CancellationToken cancellationToken) =>
        SaveChangeIfAutoSaveAsync(true, cancellationToken);

    public List<TEntity> ToList(Expression<Func<TEntity, bool>> predicate) =>
        DbSet.Where(predicate).ToList();

    public Task<List<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default) =>
        DbSet.Where(predicate).ToListAsync(cancellationToken);

    public TEntity Update(TEntity entity, bool autoSave = false)
    {
        DbSet.Update(entity);
        SaveChangeIfAutoSave(autoSave);
        return entity;
    }

    public List<TEntity> Update(IEnumerable<TEntity> entities, bool autoSave = false)
    {
        var data = entities as List<TEntity> ?? entities.ToList();
        DbSet.UpdateRange(data);
        SaveChangeIfAutoSave(autoSave);
        return data;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false,
        CancellationToken cancellationToken = default)
    {
        DbSet.Update(entity);
        await SaveChangeIfAutoSaveAsync(autoSave, cancellationToken);
        return entity;
    }

    public async Task<List<TEntity>> UpdateAsync(IEnumerable<TEntity> entities, bool autoSave = false,
        CancellationToken cancellationToken = default)
    {
        var data = entities as List<TEntity> ?? entities.ToList();
        DbSet.UpdateRange(data);
        await SaveChangeIfAutoSaveAsync(autoSave,cancellationToken);
        return data;
    }
}