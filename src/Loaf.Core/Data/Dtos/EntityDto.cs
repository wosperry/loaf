namespace Loaf.Core.Data.Dtos
{
    public class EntityDto<TKey> where TKey : struct
    {
        public TKey Id { get; set; }
    }
}