namespace Loaf.EntityFrameworkCore.SoftDelete;

public interface ISoftDelete
{
    public bool IsDeleted { get; set; }
}