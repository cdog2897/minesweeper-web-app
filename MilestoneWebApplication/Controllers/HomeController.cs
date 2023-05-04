using Microsoft.AspNetCore.Mvc;
using MilestoneWebApplication.Models;
using RegisterAndLoginApp.Services;
using System.Diagnostics;

namespace MilestoneWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }




        // landing page, if logged in go to "menu"
        public IActionResult Index()
        {
            SecurityDAO dao = new SecurityDAO();
            string userSession = HttpContext.Session.GetString("username");
            if(userSession == null)
            {
                return View();
            }
            else
            {
                User user = dao.FindUserByName(userSession);

                return View("Menu", user);
            }
        }


        public IActionResult DeleteGame(int gameToDelete)
        {
            Console.WriteLine("Deleting game");
            SecurityDAO dao = new SecurityDAO();
            //int bN = int.Parse(gameToDelete);


            string user = HttpContext.Session.GetString("username");

            User userSession = dao.FindUserByName(user);

            userSession.savedGames.RemoveAt(gameToDelete);

            int games = userSession.savedGames.Count();

            dao.UpdateUser(userSession);
           
            return View("Menu", userSession);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        // Helper controllers:
        public IActionResult Login()
        {
            return RedirectToAction("Index", "Login");
        }

        public IActionResult Registration()
        {
            return RedirectToAction("Index", "Registration");
        }




        // LOGIN CONTROLLER
        public IActionResult ProcessLogin(User user)
        {
            SecurityService securityService = new SecurityService();

            if (securityService.isValid(user))
            {
                HttpContext.Session.SetString("username", user.username);
                SecurityDAO dao = new SecurityDAO();
                User thisUser = dao.FindUserByName(user.username);

                return View("Menu", thisUser);
            }
            else
            {
                return View("LoginFailure", user);
            }
        }

    }
}