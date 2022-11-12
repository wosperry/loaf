#nullable enable

namespace Loaf.Core.Data;

public interface IPagination
{
    public int Page { get; set; }
    public int Size { get; set; }
    public string OrderBy { get; set; }
}