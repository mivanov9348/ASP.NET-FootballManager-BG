namespace ASP.NET_FootballManager.Controllers
{
    using ASP.NET_FootballManager.Data.DataModels;
    using ASP.NET_FootballManager.Models;
    using ASP.NET_FootballManager.Services.Common;
    using ASP.NET_FootballManager.Services.Game;
    using ASP.NET_FootballManager.Services.League;
    using ASP.NET_FootballManager.Services.Manager;
    using ASP.NET_FootballManager.Services.Player;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Security.Claims;

    public class MenuController : Controller
    {
        private readonly ICommonService commonService;
        private readonly IManagerService managerService;
        private readonly ILeagueService leagueService;
        private readonly IGameService gameService;
        private readonly IPlayerService playerService;
        public MenuController(IPlayerService playerService, IGameService gameService, ICommonService commonService, ILeagueService leagueService, IManagerService managerService)
        {
            this.commonService = commonService;
            this.leagueService = leagueService;
            this.managerService = managerService;
            this.gameService = gameService;
            this.playerService = playerService;
        }

        public IActionResult Inbox(int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();
            var currentInboxMessage = commonService.GetInboxMessages(CurrentGame.Id);

            return View(new InboxViewModel
            {
                News = currentInboxMessage.OrderByDescending(x => x.Id).ToList()
            });
        }

        public IActionResult DeleteNews(int id)
        {
            commonService.DeleteNews(id);
            return RedirectToAction("Inbox");
        }
        public IActionResult NextMatch()
        {
            return View();
        }

        public IActionResult Fixtures(FixturesViewModel fvm)
        {
            return View(new FixturesViewModel
            {
                Leagues = leagueService.GetAllLeagues(),
                Fixtures = commonService.GetFixture(fvm.LeagueId)
            });
        }


        public IActionResult Standings(StandingsViewModel svm)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();

            if (svm.LeagueId == 0)
            {
                svm.Leagues = leagueService.GetAllLeagues();
                svm.VirtualTeams = leagueService.GetStandingsByLeague(svm.LeagueId);
            }
            else
            {
                svm.Leagues = leagueService.GetAllLeagues();
                svm.VirtualTeams = leagueService.GetStandingsByLeague(svm.LeagueId);
            }

            return View(svm);

        }
        public IActionResult PlayersStats(PlayersViewModel pvm)
        {
            pvm = playerService.SortingPlayers(pvm.SortBy);
            return View(pvm);
        }
        public IActionResult TeamStats()
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();
            var originalTeam = commonService.GetOriginalTeam(currentTeam);

            return View(new TeamViewModel
            {
                Team = originalTeam,
                CurrentTeam = currentTeam,
                Players = playerService.GetPlayersByTeam(currentTeam.Id),
                Nations = commonService.GetAllNations(),
                Teams = commonService.GetAllVirtualTeams(),
                Cities = commonService.GetAllCities(),
                Positions = commonService.GetAllPositions(),
                Leagues = leagueService.GetAllLeagues()
            });
        }

        private (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) CurrentGameInfo()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var currentManager = managerService.GetCurrentManager(userId);
            var currentGame = gameService.GetCurrentGame(currentManager.Id);
            var currentTeam = commonService.GetCurrentTeam(currentGame);
            return (userId, currentManager, currentGame, currentTeam);
        }

    }
}
