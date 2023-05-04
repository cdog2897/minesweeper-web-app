using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MilestoneWebApplication.Models;
using Newtonsoft.Json;
using RegisterAndLoginApp.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MilestoneWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameAPIController : ControllerBase
    {
        SecurityDAO dao = new SecurityDAO();


        [HttpGet]
        [CustomAuthorization]
        public ActionResult<String> Index()
        {

            // concat json to user.savedGames
            string userSession = HttpContext.Session.GetString("username");
            User user = dao.FindUserByName(userSession);


            List<Board> boards = user.savedGames;
            string returnThis = JsonConvert.SerializeObject(boards);

            return returnThis;
        }

        [HttpGet("showOneGame/{id}")]
        [CustomAuthorization]
        public ActionResult<String> ShowOneGame(int id)
        {
            // concat json to user.savedGames
            string userSession = HttpContext.Session.GetString("username");
            User user = dao.FindUserByName(userSession);


            string returnThis = JsonConvert.SerializeObject(user.savedGames.ElementAt(id));
            return returnThis;
        }

        [HttpGet("deleteOneGame/{id}")]
        [CustomAuthorization]
        public ActionResult<String> DeleteOneGame(int id)
        {
            // concat json to user.savedGames
            string userSession = HttpContext.Session.GetString("username");
            User user = dao.FindUserByName(userSession);

            try
            {
                user.savedGames.RemoveAt(id);
                dao.UpdateUser(user);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return JsonConvert.SerializeObject(user.savedGames);
        }


    }
}

