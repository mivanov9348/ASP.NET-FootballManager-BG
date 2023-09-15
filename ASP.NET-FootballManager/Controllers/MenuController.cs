namespace ASP.NET_FootballManager.Controllers
{
    using FootballManager.Core.Models.Menu;
    using FootballManager.Core.Services;
    using Microsoft.AspNetCore.Mvc;
    using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
    using System.Security.Claims;

    public class MenuController : Controller
    {
        private readonly ServiceAggregator serviceAggregator;
        private string userId;
        public MenuController(ServiceAggregator serviceAggregator)
        {
            this.serviceAggregator = serviceAggregator;
        }

        public IActionResult Index()
        {
            CurrentUser();
            var currentGame = serviceAggregator.gameService.GetCurrentGame(userId);
            var menuViewModel = serviceAggregator.modelService.GetMenuViewModel(currentGame);
            return View(menuViewModel);
        }
        private void CurrentUser()
        {
            if (this.User.Identity.IsAuthenticated != false)
            {
                userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
        }
    }
}
