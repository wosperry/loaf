using Microsoft.EntityFrameworkCore;

namespace Loaf.EntityFrameworkCore;

public interface IModelCustomizing
{
    void Customize(ModelBuilder modelBuilder, DbContext context);
}
