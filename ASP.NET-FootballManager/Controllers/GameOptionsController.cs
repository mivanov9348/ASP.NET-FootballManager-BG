namespace ASP.NET_FootballManager.Controllers
{
    using FootballManager.Core.Models.GameOptions;
    using FootballManager.Core.Services.GameOption;
    using Microsoft.AspNetCore.Mvc;
    public class GameOptionsController : Controller
    {
        private IGameOptionService gameOptionService;
        public GameOptionsController(IGameOptionService gameOptionService)
        {
            this.gameOptionService = gameOptionService;
        }

        public IActionResult Index()
        {
            return View(new GameOptionsViewModel());
        }

        public IActionResult SaveOptions(GameOptionsViewModel model)
        {
            gameOptionService.SaveOptions(this.User, model);
            return View("Index");
        }
    }
}
