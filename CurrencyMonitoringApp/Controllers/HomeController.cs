using Microsoft.AspNetCore.Mvc;

namespace CurrencyMonitoringApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
