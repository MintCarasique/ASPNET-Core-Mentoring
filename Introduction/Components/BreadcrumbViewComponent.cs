using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Northwind.Components
{
    public class BreadcrumbViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            ViewBag.Controller = ViewContext.RouteData.Values["controller"];
            ViewBag.Action = ViewContext.RouteData.Values["action"];
            return View();
        }
    }
}
