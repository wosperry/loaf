namespace Loaf.Core.CommonExtensions
{
    public static class StringExtensions
    {
        public static bool IsNotEmpty(this string value)
        {
            // 字符串不为空时返回true
            return !string.IsNullOrEmpty(value);
        }
    }
}