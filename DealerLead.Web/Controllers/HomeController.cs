using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DealerLead.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DealerLead.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AuthHelper _authHelper;

        public HomeController(ILogger<HomeController> logger, AuthHelper authHelper)
        {
            _logger = logger;
            _authHelper = authHelper;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var data = _authHelper.LoginDealerUser(User);

            foreach (var key in data.Keys)
                ViewData[key] = data[key];
            
            return View("Index");
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
