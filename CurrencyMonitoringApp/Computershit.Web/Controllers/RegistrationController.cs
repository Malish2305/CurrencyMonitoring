using Computershit.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Computershit.Core.Service;

namespace Computershit.Web.Controllers
{
    public class RegistrationController : Controller
    {
        UserDataProvider _userDataProvider = new UserDataProvider();
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public async Task<bool> Login(string Login, string Password)
        {
            return await _userDataProvider.IsUserLogin(Login, Password);
        }
    }
}
