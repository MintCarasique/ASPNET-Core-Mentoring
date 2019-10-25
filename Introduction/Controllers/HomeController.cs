using Microsoft.AspNetCore.Mvc;

namespace Northwind.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
