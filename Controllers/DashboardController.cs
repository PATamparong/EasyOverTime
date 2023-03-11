using Microsoft.AspNetCore.Mvc;
using EasyOverTime.Models;
using System.Diagnostics;
using EasyOverTime.Auth;

namespace EasyOverTime.Controllers
{
    public class DashboardController : Controller
    {
   
        public IActionResult Dashboard()
        {
            return View("Index");
        }    

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
