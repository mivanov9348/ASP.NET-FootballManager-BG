namespace ASP.NET_FootballManager.Controllers
{
    using FootballManager.Core.Models.GameOptions;
    using FootballManager.Core.Services;
    using Microsoft.AspNetCore.Mvc;
    public class GameOptionsController : Controller
    {
        private ServiceAggregator serviceAggregator;
        public GameOptionsController(ServiceAggregator serviceAggregator)
        {
            this.serviceAggregator = serviceAggregator;
        }

        public IActionResult Index()
        {
            return View(new GameOptionsViewModel());
        }

        public IActionResult SaveOptions(GameOptionsViewModel model)
        {
            serviceAggregator.gameOptionsService.SaveOptions(this.User, model);
            return View("Index");
        }
    }
}
