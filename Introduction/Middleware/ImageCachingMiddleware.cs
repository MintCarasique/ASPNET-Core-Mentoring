using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Northwind.Common;
using Northwind.Services;

namespace Northwind.Middleware
{
    public class ImageCachingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ImageCachingMiddleware> _logger;

        private readonly IConfigurationService _configurationService;

        private readonly Timer _timer;

        public ImageCachingMiddleware(RequestDelegate next, ILogger<ImageCachingMiddleware> logger, IConfigurationService configurationService)
        {
            _next = next;
            _logger = logger;
            _configurationService = configurationService;

            if (!Directory.Exists(_configurationService.CachePath))
                Directory.CreateDirectory(_configurationService.CachePath);

            _timer = new Timer(EraseCache, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(_configurationService.CacheLifeTime));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _timer.Change(_configurationService.CacheLifeTime * 1000, 0);

            if (context.Request.GetUri().Segments.All(x => x.Trim('/', '\\').ToLower() != Constants.IMAGES))
            {
                await _next(context);
                return;
            }

            var originalBody = context.Response.Body;

            try
            {
                using (var memStream = new MemoryStream())
                {
                    var segments = context.Request.GetUri().Segments;
                    var filePath = $"{_configurationService.CachePath}{segments[segments.Length - 1]}.bmp";

                    if (File.Exists(filePath))
                    {
                        using (var file = new FileStream(filePath, FileMode.Open))
                        {
                            await file.CopyToAsync(originalBody);
                        }

                        return;
                    }

                    context.Response.Body = memStream;

                    await _next(context);

                    if (context.Response.ContentType == Constants.CONTENT_TYPE_BMP
                        && _configurationService.CachedImagesCount > Directory.GetFiles(_configurationService.CachePath).Length)
                    {
                        using (var file = new FileStream(filePath, FileMode.Create))
                        {
                            memStream.Position = 0;
                            await memStream.CopyToAsync(file);
                        }
                    }

                    memStream.Position = 0;
                    await memStream.CopyToAsync(originalBody);
                }
            }
            finally
            {
                context.Response.Body = originalBody;
            }
        }

        private void EraseCache(object state)
        {
            var dir = new DirectoryInfo(_configurationService.CachePath);
            foreach (var file in dir.GetFiles())
            {
                file.Delete();
            }
        }
    }
}
