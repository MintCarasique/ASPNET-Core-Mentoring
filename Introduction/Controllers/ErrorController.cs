using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Introduction.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult ExceptionError()
        {
            var error = this.HttpContext.Features.Get<IExceptionHandlerFeature>().Error;
            ViewBag.ExceptionName = error.GetType().Name;
            ViewBag.ExceptionMessage = error.Message;

            return View();
        }

        public IActionResult HttpError() 
        {
            return View();
        }
    }
}