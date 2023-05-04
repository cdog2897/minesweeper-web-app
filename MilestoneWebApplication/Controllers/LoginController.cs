using Microsoft.AspNetCore.Mvc;
using MilestoneWebApplication.Models;
using RegisterAndLoginApp.Services;

namespace MilestoneWebApplication.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ProcessLogin(User user)
        {
            SecurityService securityService = new SecurityService();
            Console.WriteLine(securityService.isValid);
            if (securityService.isValid(user))
            {
                HttpContext.Session.SetString("username", user.username);
                return RedirectToAction("Index", "Game");
            }
            else
            {
                return View("LoginFailure", user);
            }
        }
    }
}
