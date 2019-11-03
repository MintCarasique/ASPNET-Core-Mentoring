using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Services
{
    public interface IConfigurationService
    {
        int MaxProductCount { get; }

        string CachePath { get; }

        int CachedImagesCount { get; }

        int CacheLifeTime { get; }
    }
}
