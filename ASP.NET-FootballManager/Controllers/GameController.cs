namespace ASP.NET_FootballManager.Controllers
{
    using Data.Constant;
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
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Game;

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
        public GameController(IDayService dayService, ICupService cupService, IEuroCupService euroCupService, IFixtureService fixtureService, IInboxService inboxService, ITeamService teamService, IMatchService matchService, ILeagueService leagueService, IPlayerService playerService, ICommonService commonService, IManagerService managerService, IGameService gameService)
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
            this.dayService = dayService;
        }
        public async Task<IActionResult> SeasonStats(EndSeasonViewModel esvm)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = await CurrentGameInfo();
            var goalScorer = await playerService.GetLeagueGoalscorer(CurrentGame, esvm.LeagueId);
            var teams = await leagueService.GetStandingsByLeague(esvm.LeagueId, CurrentGame);
            var league = await leagueService.GetLeague(esvm.LeagueId);
            var euroCup = await euroCupService.GetEuropeanCup(esvm.EuroCupId);
            var cup = await cupService.GetCurrentCup();
            var cupWinner = await cupService.GetWinner(CurrentGame);
            var championsCupWinner = await euroCupService.GetChampionsCupWinner(CurrentGame);
            var euroCupWinner = await euroCupService.GetEuroCupWinner(CurrentGame);

            return View(new EndSeasonViewModel
            {
                GoalScorer = goalScorer,
                Leagues = await leagueService.GetAllLeagues(),
                EuroCups = await euroCupService.AllEuroCups(),
                EuroCup = euroCup,
                Teams = teams,
                League = league,
                Cup = cup,
                CupWinner = cupWinner,
                EuroCupWinner = euroCupWinner,
                ChampionsCupWinner = championsCupWinner
            });
        }
        public async Task<IActionResult> NewSeason()
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = await CurrentGameInfo();

            await leagueService.PromotedRelegated(CurrentGame);
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
            playerService.CalculatingPlayersPrice(CurrentGame);
            playerService.UpdateAttributes(CurrentGame);
            playerService.ResetPlayerStats(CurrentGame);
            teamService.ResetTeams(CurrentGame);
            inboxService.NewSeasonNews(CurrentGame);

            return RedirectToAction("Inbox", "Menu");
        }
        public async Task<IActionResult> EndSeason(EndSeasonViewModel esvm)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = await CurrentGameInfo();
            var teams = await leagueService.GetStandingsByLeague(esvm.LeagueId, CurrentGame);

            return View(new EndSeasonViewModel
            {
                Leagues = await leagueService.GetAllLeagues(),
                Teams = teams
            });
        }
        private async Task<(string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam)> CurrentGameInfo()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var currentManager = managerService.GetCurrentManager(userId);
            var currentGame = gameService.GetCurrentGame(currentManager.Id);
            var currentTeam = await teamService.GetCurrentTeam(currentGame);
            return (userId, currentManager, currentGame, currentTeam);
        }

    }





}
