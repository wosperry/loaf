namespace Loaf.EntityFrameworkCore.SoftDelete;

public interface ISoftDeleteHandler
{
    public void SoftDeleteExecuting<TEntity>(TEntity entity) where TEntity:class;
}