namespace Loaf.Core.CommonExtensions
{
    public static class StringExtensions
    {
        public static bool IsNotEmpty(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}