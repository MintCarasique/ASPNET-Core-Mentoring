using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Northwind.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int MaxProductCount => _configuration.GetSection("ProductParams").GetValue<int>("MaxAmountPerRequest");
        public string CachePath => _configuration.GetSection("Cache").GetValue<string>("CachePath");
        public int CachedImagesCount => _configuration.GetSection("Cache").GetValue<int>("CachedImagesCount");
        public int CacheLifeTime => _configuration.GetSection("Cache").GetValue<int>("CacheLifeTime");
    }
}
