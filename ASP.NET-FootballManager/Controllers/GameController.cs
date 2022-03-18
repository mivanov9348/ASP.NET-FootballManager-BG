namespace ASP.NET_FootballManager.Controllers
{
    using Data.Constant;
    using ASP.NET_FootballManager.Data.DataModels;
    using ASP.NET_FootballManager.Models;
    using ASP.NET_FootballManager.Services.Common;
    using ASP.NET_FootballManager.Services.Game;
    using ASP.NET_FootballManager.Services.League;
    using ASP.NET_FootballManager.Services.Manager;
    using ASP.NET_FootballManager.Services.Player;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using ASP.NET_FootballManager.Services.Match;
    using ASP.NET_FootballManager.Services.Team;

    public class GameController : Controller
    {
        private readonly IPlayerService playerService;
        private readonly ICommonService commonService;
        private readonly IManagerService managerService;
        private readonly IGameService gameService;
        private readonly ILeagueService leagueService;
        private readonly IMatchService matchService;
        private readonly ITeamService teamService;
        public GameController(ITeamService teamService,IMatchService matchService,ILeagueService leagueService, IPlayerService playerService, ICommonService commonService, IManagerService managerService, IGameService gameService)
        {
            this.playerService = playerService;
            this.commonService = commonService;
            this.managerService = managerService;
            this.gameService = gameService;
            this.leagueService = leagueService;
            this.matchService = matchService;
            this.teamService = teamService;
        }

        public IActionResult EndSeason(EndSeasonViewModel esvm)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();
            var teams = leagueService.GetStandingsByLeague(esvm.LeagueId);

            return View(new EndSeasonViewModel
            {
                Leagues = leagueService.GetAllLeagues(),
                Teams = teams
            });
        }

        public IActionResult SeasonStats(EndSeasonViewModel esvm)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();
            var goalScorer = playerService.GetGoalscorer(CurrentGame);
            var teams = leagueService.GetStandingsByLeague(esvm.LeagueId);
            var league = leagueService.GetLeague(esvm.LeagueId);

            return View(new EndSeasonViewModel
            {
                GoalScorer = goalScorer,
                Leagues = leagueService.GetAllLeagues(),
                Teams = teams,
                League = league
            });
        }

        public IActionResult NewSeason()
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();

            leagueService.PromotedRelegated(CurrentGame);
            matchService.DeleteMatches(CurrentGame);
            leagueService.GenerateFixtures(CurrentGame);
            playerService.CreateFreeAgents(CurrentGame, DataConstants.FreeAgents.gk, DataConstants.FreeAgents.df, DataConstants.FreeAgents.mf, DataConstants.FreeAgents.st);
            teamService.ResetTeams(CurrentGame);
            gameService.ResetGame(CurrentGame);

            return RedirectToAction("Inbox", "Menu");
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
