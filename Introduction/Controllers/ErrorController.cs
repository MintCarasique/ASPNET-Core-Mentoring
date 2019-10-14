using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Introduction.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }
        public IActionResult ExceptionError()
        {
            var error = this.HttpContext.Features.Get<IExceptionHandlerFeature>().Error;

            ViewBag.ExceptionName = error.GetType().Name;
            ViewBag.ExceptionMessage = error.Message;
            _logger.LogError($"{ViewBag.ExceptionName} was thrown! Message: {ViewBag.ExceptionMessage}");
            return View();
        }

        public IActionResult HttpError() 
        {
            return View();
        }
    }
}