using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace Northwind.Middleware
{
    public static class ImageCachingMiddlewareExtension
    {
        public static IApplicationBuilder UseImageCaching(this IApplicationBuilder app)
        {
            app.UseMiddleware<ImageCachingMiddleware>();
            return app;
        }
    }
}
