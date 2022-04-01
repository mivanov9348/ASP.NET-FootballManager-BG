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
    using ASP.NET_FootballManager.Services.Inbox;
    using ASP.NET_FootballManager.Services.Fixture;
    using ASP.NET_FootballManager.Services.EuroCup;
    using ASP.NET_FootballManager.Services.Cup;

    public class GameController : Controller
    {
        private readonly IPlayerService playerService;
        private readonly ICommonService commonService;
        private readonly IManagerService managerService;
        private readonly IGameService gameService;
        private readonly ILeagueService leagueService;
        private readonly IMatchService matchService;
        private readonly ITeamService teamService;
        private readonly IInboxService inboxService;
        private readonly IFixtureService fixtureService;
        private readonly ICupService cupService;
        private readonly IEuroCupService euroCupService;
        private readonly IDayService dayService;
        public GameController(IDayService dayService,ICupService cupService, IEuroCupService euroCupService, IFixtureService fixtureService, IInboxService inboxService, ITeamService teamService, IMatchService matchService, ILeagueService leagueService, IPlayerService playerService, ICommonService commonService, IManagerService managerService, IGameService gameService)
        {
            this.playerService = playerService;
            this.commonService = commonService;
            this.managerService = managerService;
            this.gameService = gameService;
            this.leagueService = leagueService;
            this.matchService = matchService;
            this.teamService = teamService;
            this.inboxService = inboxService;
            this.fixtureService = fixtureService;
            this.euroCupService = euroCupService;
            this.cupService = cupService;
            this.dayService= dayService;
        }
        public IActionResult SeasonStats(EndSeasonViewModel esvm)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();
            var goalScorer = playerService.GetLeagueGoalscorer(CurrentGame, esvm.LeagueId);
            var teams = leagueService.GetStandingsByLeague(esvm.LeagueId);
            var league = leagueService.GetLeague(esvm.LeagueId);
            var euroCup = euroCupService.GetEuropeanCup(esvm.EuroCupId);
            var cup = cupService.GetCurrentCup();
            var cupWinner = cupService.GetWinner(CurrentGame);
            var championsCupWinner = euroCupService.GetChampionsCupWinner(CurrentGame);
            var euroCupWinner = euroCupService.GetEuroCupWinner(CurrentGame);

            return View(new EndSeasonViewModel
            {
                GoalScorer = goalScorer,
                Leagues = leagueService.GetAllLeagues(),
                EuroCups = euroCupService.AllEuroCups(),
                EuroCup = euroCup,
                Teams = teams,
                League = league,
                Cup = cup,
                CupWinner = cupWinner,
                EuroCupWinner = euroCupWinner,
                ChampionsCupWinner = championsCupWinner
            });
        }
        public IActionResult NewSeason()
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();

            leagueService.PromotedRelegated(CurrentGame);
            gameService.ResetGame(CurrentGame);
            dayService.CalculateDays(CurrentGame);
            fixtureService.AddFixtureToDay(CurrentGame);
            matchService.DeleteMatches(CurrentGame);
            fixtureService.DeleteFixtures(CurrentGame);
            fixtureService.GenerateLeagueFixtures(CurrentGame);
            cupService.GenerateCupParticipants(CurrentGame);
            fixtureService.GenerateCupFixtures(CurrentGame);
            euroCupService.DistributionEuroParticipant(CurrentGame);
            fixtureService.GenerateEuroFixtures(CurrentGame);
            playerService.CreateFreeAgents(CurrentGame, DataConstants.FreeAgents.gk, DataConstants.FreeAgents.df, DataConstants.FreeAgents.mf, DataConstants.FreeAgents.st);
            teamService.ResetTeams(CurrentGame);           
            inboxService.NewSeasonNews(CurrentGame);

            return RedirectToAction("Inbox", "Menu");
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
        private (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) CurrentGameInfo()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var currentManager = managerService.GetCurrentManager(userId);
            var currentGame = gameService.GetCurrentGame(currentManager.Id);
            var currentTeam = teamService.GetCurrentTeam(currentGame);
            return (userId, currentManager, currentGame, currentTeam);
        }

    }





}
