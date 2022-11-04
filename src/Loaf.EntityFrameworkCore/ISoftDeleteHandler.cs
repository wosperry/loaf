namespace Loaf.EntityFrameworkCore;

public interface ISoftDeleteHandler
{
    public void SoftDeleteExecuting<TEntity>(TEntity entity) where TEntity:class;
}