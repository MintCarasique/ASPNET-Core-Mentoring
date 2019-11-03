using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Northwind.Filters
{
    public class LoggingFilterAttribute : ActionFilterAttribute
    {
        private readonly ILogger<LoggingFilterAttribute> _logger;

        public LoggingFilterAttribute(ILogger<LoggingFilterAttribute> logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation($"Begin:{context.ActionDescriptor.DisplayName}");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation($"End: {context.ActionDescriptor.DisplayName}");
        }
    }
}
