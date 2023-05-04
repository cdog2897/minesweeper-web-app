using Microsoft.AspNetCore.Mvc;
using MilestoneWebApplication.Models;
using RegisterAndLoginApp.Services;
using Newtonsoft.Json;
using static System.Reflection.Metadata.BlobBuilder;


namespace MilestoneWebApplication.Controllers
{
    public class GameController : Controller
    {

        static Board GameBoard = new Board(20, 20);

        [CustomAuthorization]
        public IActionResult Index()
        {
            string user = HttpContext.Session.GetString("username");
            List<Cell> cells = new List<Cell>();
            GameBoard = new Board(20, 20);
            GameBoard.Grid = GameBoard.createBoard(40);
            
            return View("Index", GameBoard);
        }

        public IActionResult LoadGame(int gameToLoad)
        {
            SecurityDAO dao = new SecurityDAO();
            //int bN = int.Parse(gameToLoad);

            string user = HttpContext.Session.GetString("username");

            GameBoard = dao.LoadGame(user, gameToLoad);
            
            return View("Index", GameBoard);
        }

        public int buttonNumberToX(int buttonNumber)
        {
            return buttonNumber / 20;
        }

        public int buttonNumberToY(int buttonNumber)
        {
            return buttonNumber % 20;
        }

        public IActionResult RightClickShowOneButton(string buttonNumber)
        {
            int id = Int32.Parse(buttonNumber);
            int x = buttonNumberToX(id);
            int y = buttonNumberToY(id);

            GameBoard.Grid[x, y].Flag = !GameBoard.Grid[x, y].Flag;
            return PartialView(GameBoard);
        }

        public void FloodFill(string buttonNumber)
        {
            int id = Int32.Parse(buttonNumber);
            int x = buttonNumberToX(id);
            int y = buttonNumberToY(id);
            Console.WriteLine("GameController Floodfill: " + id);
            GameBoard.floodFill(x, y);
        }

        public IActionResult ShowGameStatus()
        {
            GameBoard.checkGame();
            return PartialView(GameBoard);
        }

        public IActionResult SaveGameStatus()
        {
            SecurityDAO dao = new SecurityDAO();

            // Serialize Board to json
            string currentBoard = JsonConvert.SerializeObject(GameBoard);

            // concat json to user.savedGames
            string userSession = HttpContext.Session.GetString("username");
            User user = dao.FindUserByName(userSession);

            GameBoard.checkGame();
            GameBoard.TimeSaved = DateTime.Now;

            if (user.savedGames == null)
            {
                user.savedGames = new List<Board>();
            }
            GameBoard.Id = user.savedGames.Count();
            user.savedGames.Add(GameBoard);

            // update user.savedGames to database
            dao.UpdateUser(user);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult UpdateGrid()
        {
            GameBoard.checkGame();
            return PartialView(GameBoard);
        }



    }
}
