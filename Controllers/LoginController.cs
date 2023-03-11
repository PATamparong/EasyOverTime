using Microsoft.AspNetCore.Mvc;
using EasyOverTime.Models;
using System.Diagnostics;


namespace EasyOverTime.Controllers
{

    public class LoginController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public IActionResult Login()
        {
            var token = HttpContext.Session.GetString("token");

            if(token == null )
            {
                return View("Index");                
            }
            return RedirectToAction("./Dashboard");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
