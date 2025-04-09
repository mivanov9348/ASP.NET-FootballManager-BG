namespace ASP.NET_FootballManager.Controllers
{
    using FootballManager.Core.Models.Team;
    using FootballManager.Core.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class TeamsController : Controller
    {
        private readonly ServiceAggregator serviceAggregator;

        public TeamsController(ServiceAggregator serviceAggregator)
        {
            this.serviceAggregator = serviceAggregator;
        }

        public async Task<IActionResult> Standings(StandingsViewModel svm)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var (userIdResult, currentManager, currentGame, currentTeam) = serviceAggregator.gameService.CurrentGameInfo(userId);
            var menuViewModel = serviceAggregator.modelService.GetMenuViewModel(currentGame);

            svm.Leagues = await serviceAggregator.leagueService.GetAllLeagues();
            svm.VirtualTeams = await serviceAggregator.leagueService.GetStandingsByLeague(svm.LeagueId == 0 ? 0 : svm.LeagueId, currentGame);
            svm.MenuViewModel = menuViewModel;

            return View(svm);
        }

        public async Task<IActionResult> TeamStats()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var (userIdResult, currentManager, currentGame, currentTeam) = serviceAggregator.gameService.CurrentGameInfo(userId);
            var model = serviceAggregator.modelService.GetTeamViewModel(currentTeam);

            return View(model);
        }
    }
}