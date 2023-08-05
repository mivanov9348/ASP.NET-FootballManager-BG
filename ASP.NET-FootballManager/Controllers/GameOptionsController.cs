namespace ASP.NET_FootballManager.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    public class GameOptionsController : Controller
    {
        public GameOptionsController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateOptions()
        {
            return View();
        }
    }
}
