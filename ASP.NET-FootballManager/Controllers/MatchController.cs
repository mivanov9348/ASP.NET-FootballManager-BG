namespace ASP.NET_FootballManager.Controllers
{
    using ASP.NET_FootballManager.Data.DataModels;
    using ASP.NET_FootballManager.Models;
    using ASP.NET_FootballManager.Services.Common;
    using ASP.NET_FootballManager.Services.Game;
    using ASP.NET_FootballManager.Services.League;
    using ASP.NET_FootballManager.Services.Manager;
    using ASP.NET_FootballManager.Services.Match;
    using ASP.NET_FootballManager.Services.Player;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    public class MatchController : Controller
    {
        private readonly IMatchService matchService;
        private readonly IGameService gameService;
        private readonly ICommonService commonService;
        private readonly IManagerService managerService;
        private readonly ILeagueService leagueService;
        private readonly IPlayerService playerService;
        public MatchController(IMatchService matchService,
        IGameService gameService,
        ICommonService commonService,
        IManagerService managerService,
        ILeagueService leagueService,
        IPlayerService playerService
            )
        {
            this.managerService = managerService;
            this.matchService = matchService;
            this.gameService = gameService;
            this.commonService = commonService;
            this.leagueService = leagueService;
            this.playerService = playerService;
        }

        public IActionResult MatchDayPreview()
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();
            var dayFixtures = matchService.GetFixturesByDay(CurrentGame);
            var round = dayFixtures.First().Round;

            return View(new MatchDayViewModel
            {
                DayFixtures = dayFixtures,
                Day = CurrentGame.Day,
                Year = CurrentGame.Year,
                Round = round,
                Leagues = leagueService.GetAllLeagues()
            });
        }
        public IActionResult MatchPreview()
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();
            var dayFixtures = matchService.GetFixturesByDay(CurrentGame);
            var currentFixture = matchService.GetCurrentFixture(dayFixtures, CurrentGame);
            var homeTeamPlayers = matchService.GetUserPlayers(currentTeam);
            var awayTeamPlayers = matchService.GetPcTeamPlayers(currentTeam, currentFixture);

            return View(new MatchViewModel
            {
                HomeTeamName = currentFixture.HomeTeamName,
                AwayTeamName = currentFixture.AwayTeamName,
                CurrentFixture = currentFixture,
                HomeTeamPlayers = matchService.GetStarting11(currentFixture.HomeTeamId),
                AwayTeamPlayers = matchService.GetStarting11(currentFixture.AwayTeamId)          
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
