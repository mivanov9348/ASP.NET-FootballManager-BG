namespace ASP.NET_FootballManager.Controllers
{
    using FootballManager.Core.Models.Team;
    using FootballManager.Core.Services;
    using FootballManager.Infrastructure.Data.DataModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    public class TeamsController : Controller
    {
        private readonly ServiceAggregator serviceAggregator;
        public TeamsController(ServiceAggregator serviceAggregator)
        {
            this.serviceAggregator = serviceAggregator;
        }
        public async Task<IActionResult> Standings(StandingsViewModel svm)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.gameService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var menuViewModel = serviceAggregator.modelService.GetMenuViewModel(CurrentGame);
            if (svm.LeagueId == 0)
            {
                svm.Leagues = await serviceAggregator.leagueService.GetAllLeagues();
                svm.VirtualTeams = await serviceAggregator.leagueService.GetStandingsByLeague(svm.LeagueId, CurrentGame);
            }
            else
            {
                svm.Leagues = await serviceAggregator.leagueService.GetAllLeagues();
                svm.VirtualTeams = await serviceAggregator.leagueService.GetStandingsByLeague(svm.LeagueId, CurrentGame);
            }
            svm.MenuViewModel = menuViewModel;
            return View(svm);
        }
        public async Task<IActionResult> TeamStats()
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.gameService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var model = serviceAggregator.modelService.GetTeamViewModel(currentTeam);

            return View(model);
        }


    }
}
