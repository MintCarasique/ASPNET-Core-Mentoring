namespace Northwind.Core.Services
{
    public interface IConfigurationService
    {
        int MaxProductCount { get; }

        string CachePath { get; }

        int CachedImagesCount { get; }

        int CacheLifeTime { get; }
    }
}
