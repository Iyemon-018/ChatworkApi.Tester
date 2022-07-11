namespace ChatworkApi.Tester.Presentation
{
    using System;
    using System.Windows;

    internal static class TemplateCache<T>
    {
        private static Func<T, DataTemplate> _cache;

        static TemplateCache()
        {
        }

        public static void Set(Func<T, DataTemplate> cache)
        {
            if (cache != null) _cache = cache;
        }

        public static DataTemplate Get(T key) => _cache?.Invoke(key);
    }
}