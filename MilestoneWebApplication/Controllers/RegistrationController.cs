using Microsoft.AspNetCore.Mvc;
using MilestoneWebApplication.Models;
using RegisterAndLoginApp.Services;

namespace MilestoneWebApplication.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessRegistration(User user)
        {
            SecurityService securityService = new SecurityService();
            if (securityService.registerSuccess(user))
            {
                return View("ProcessRegistration", user);
            }
            else
            {
                return View("RegisterFailed");
            }
        }
    }
}
