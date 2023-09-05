using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
using FootballManager.Core.Models.Team;
using FootballManager.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ASP.NET_FootballManager.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ServiceAggregator serviceAggregator;
        public TeamsController(ServiceAggregator serviceAggregator)
        {
            this.serviceAggregator = serviceAggregator;
        }
        public async Task<IActionResult> Standings(StandingsViewModel svm)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);

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

            return View(svm);
        }
        public async Task<IActionResult> TeamStats()
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var originalTeam = await serviceAggregator.teamService.GetOriginalTeam(currentTeam, CurrentGame);

            return View(new TeamViewModel
            {
                Team = originalTeam,
                CurrentTeam = currentTeam,
                Players = await serviceAggregator.playerDataService.GetPlayersByTeam(currentTeam.Id),
                Nations = await serviceAggregator.commonService.GetAllNations(),
                Teams = await serviceAggregator.teamService.GetAllVirtualTeams(CurrentGame),
                Cities = await serviceAggregator.commonService.GetAllCities(),
                Positions = await serviceAggregator.commonService.GetAllPositions(),
                Leagues = await serviceAggregator.leagueService.GetAllLeagues(),
                PlayerAttributes = await serviceAggregator.commonService.GetAllPlayersAttribute(),
                PlayerStats = await serviceAggregator.commonService.GetAllPlayersStats()
            });
        }


    }
}
