namespace Loaf.EntityFrameworkCore;

public interface ISoftDelete
{
    public bool IsDeleted { get; set; }
}