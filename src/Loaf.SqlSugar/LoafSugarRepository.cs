using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Loaf.Core.Data;

namespace Loaf.SqlSugar
{
    public class SugarRepository<TEntity> : IRepository<TEntity>
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

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<TEntity>> GetPagedResultAsync(Expression<Func<TEntity, bool>> predicate, IPagination pagination)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> GetQueryable()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> GetQueryable<TParameter>(TParameter parameter)
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

        public Task SaveChangesAsync(CancellationToken cancellationToken)
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

        public TEntity Update(TEntity entity, bool autoSave = true)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> Update(IEnumerable<TEntity> entities, bool autoSave = true)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> UpdateAsync(IEnumerable<TEntity> entities, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
