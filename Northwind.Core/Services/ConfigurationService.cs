using System;
using Microsoft.Extensions.Configuration;

namespace Northwind.Core.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int MaxProductCount => Convert.ToInt32(_configuration.GetSection("ProductParams").GetSection("MaxAmountPerRequest").Value);
        public string CachePath => _configuration.GetSection("Cache").GetSection("CachePath").Value;
        public int CachedImagesCount => Convert.ToInt32(_configuration.GetSection("Cache").GetSection("CachedImagesCount").Value);
        public int CacheLifeTime => Convert.ToInt32(_configuration.GetSection("Cache").GetSection("CacheLifeTime").Value);
    }
}
