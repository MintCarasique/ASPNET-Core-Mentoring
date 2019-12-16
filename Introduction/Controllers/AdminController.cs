using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Northwind.Controllers
{
    //[Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private UserManager<IdentityUser> _userManager;

        public AdminController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            List<IdentityUser> usersList = _userManager.Users.ToList();
            return View(usersList);
        }
    }
}