namespace ASP.NET_FootballManager.Controllers
{
    using ASP.NET_FootballManager.Data.DataModels;
    using ASP.NET_FootballManager.Models;
    using ASP.NET_FootballManager.Services.Common;
    using ASP.NET_FootballManager.Services.Cup;
    using ASP.NET_FootballManager.Services.EuroCup;
    using ASP.NET_FootballManager.Services.Fixture;
    using ASP.NET_FootballManager.Services.Game;
    using ASP.NET_FootballManager.Services.Inbox;
    using ASP.NET_FootballManager.Services.League;
    using ASP.NET_FootballManager.Services.Manager;
    using ASP.NET_FootballManager.Services.Match;
    using ASP.NET_FootballManager.Services.Player;
    using ASP.NET_FootballManager.Services.Team;
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
        private readonly IFixtureService fixtureService;
        private readonly IInboxService inboxService;
        private readonly ITeamService teamService;
        private readonly IDayService dayService;
        private readonly ICupService cupService;
        private readonly IEuroCupService euroCupService;
        public MatchController(IMatchService matchService,
        IGameService gameService,
        ICommonService commonService,
        IManagerService managerService,
        ILeagueService leagueService,
        IPlayerService playerService,
        IInboxService inboxService,
        ITeamService teamService,
        IDayService dayService,
        ICupService cupService,
        IEuroCupService euroCupService,
        IFixtureService fixtureService
            )
        {
            this.managerService = managerService;
            this.matchService = matchService;
            this.gameService = gameService;
            this.commonService = commonService;
            this.leagueService = leagueService;
            this.playerService = playerService;
            this.inboxService = inboxService;
            this.teamService = teamService;
            this.dayService = dayService;
            this.cupService = cupService;
            this.euroCupService = euroCupService;
            this.fixtureService = fixtureService;
        }
        public IActionResult MatchDayPreview()
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();
            var dayFixtures = matchService.GetFixturesByDay(CurrentGame);          

            if ( dayFixtures == null|| dayFixtures.Count == 0)
            {
                return RedirectToAction("EndSeason", "Game");
            }

            var round = dayFixtures.First().Round;

            return View(new MatchDayViewModel
            {
                DayFixtures = dayFixtures,
                Day = CurrentGame.Day,
                Year = CurrentGame.Year,
                Round = round,
                Leagues = leagueService.GetAllLeagues(),
                CurrentTeam = currentTeam
            });
        }
        public IActionResult MatchPreview()
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();
            var dayFixtures = matchService.GetFixturesByDay(CurrentGame);
            var currentFixture = matchService.GetCurrentFixture(dayFixtures, CurrentGame);           

            return View(new MatchViewModel
            {
                Positions = commonService.GetAllPositions(),
                HomeTeamName = currentFixture.HomeTeamName,
                AwayTeamName = currentFixture.AwayTeamName,
                CurrentFixture = currentFixture,
                HomeTeamPlayers = matchService.GetStarting11(currentFixture.HomeTeamId).OrderBy(x=>x.PositionId).ToList(),
                AwayTeamPlayers = matchService.GetStarting11(currentFixture.AwayTeamId).OrderBy(x => x.PositionId).ToList()
            });
        }
        public IActionResult Tactics()
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();
            var clubStartingEleven = playerService.GetStartingEleven(currentTeam.Id).OrderBy(x => x.PositionId).ToList();
            var clubSubstitutes = playerService.GetSubstitutes(currentTeam.Id).OrderBy(x => x.PositionId).ToList();
            var positions = commonService.GetAllPositions();
            var dayFixtures = matchService.GetFixturesByDay(CurrentGame);
            var currentFixture = matchService.GetCurrentFixture(dayFixtures, CurrentGame);
            var currentDay = dayService.GetCurrentDay(CurrentGame);

            if (currentFixture == null)
            {
                if (currentDay.isCupDay)
                {
                    cupService.CalculateOtherMatches(dayFixtures, currentFixture);
                    fixtureService.GenerateCupFixtures(CurrentGame);                    
                }
                if (currentDay.isEuroCupDay)
                {
                    euroCupService.CalculateOtherMatches(dayFixtures, currentFixture);
                    fixtureService.GenerateEuroFixtures(CurrentGame);
                }
                inboxService.CupMatchesInfo(dayFixtures,CurrentGame);
                gameService.NextDay(CurrentGame);
                return RedirectToAction("Results", "Match");
            }

            return View(new TacticsViewModel
            {
                CurrentTeam = currentTeam,
                Substitutes = clubSubstitutes,
                StartingEleven = clubStartingEleven,
                Positions = positions
            });
        }
        public IActionResult Match(MatchViewModel mvm)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();
            var dayFixtures = matchService.GetFixturesByDay(CurrentGame);
            var currentFixture = matchService.GetCurrentFixture(dayFixtures, CurrentGame);
            var homeTeamPlayers = matchService.GetStarting11(currentFixture.HomeTeamId).OrderBy(x => x.PositionId).ToList();
            var awayTeamPlayers = matchService.GetStarting11(currentFixture.AwayTeamId).OrderBy(x => x.PositionId).ToList();
            var currentMatch = matchService.CreateMatch(currentFixture, CurrentGame);
            var newModel = matchService.GetMatchModel(currentMatch, currentFixture, homeTeamPlayers.First());
            return View(newModel);
        }
        public IActionResult GetAction(int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();
            var currentMatch = matchService.GetCurrentMatch(id);
            var dayFixtures = matchService.GetFixturesByDay(CurrentGame);
            var currentFixture = matchService.GetCurrentFixture(dayFixtures, CurrentGame);
            var currentDay = dayService.GetCurrentDay(CurrentGame);
            var player = new Player();
            
            matchService.Time(currentMatch);
            if (currentMatch.Minute > 90)
            {
                matchService.EndMatch(currentMatch);
                if (currentDay.isLeagueDay)
                {
                    leagueService.CheckWinner(currentFixture.HomeTeamGoal, currentFixture.AwayTeamGoal, currentFixture);
                    leagueService.CalculateOtherMatches(dayFixtures, currentFixture);
                }
                if (currentDay.isCupDay)
                {
                    cupService.CheckWinner(currentFixture);
                    cupService.CalculateOtherMatches(dayFixtures, currentFixture);
                    fixtureService.GenerateCupFixtures(CurrentGame);
                }
                if (currentDay.isEuroCupDay)
                {
                    euroCupService.CheckWinner(currentFixture);
                    euroCupService.CalculateOtherMatches(dayFixtures, currentFixture);
                    fixtureService.GenerateEuroFixtures(CurrentGame);
                }
                gameService.NextDay(CurrentGame);
                inboxService.MatchFinishedNews(CurrentGame, currentFixture);
                return RedirectToAction("Results");
            }

            if (currentMatch.Turn == 1)
            {
                var homeTeam = teamService.GetTeamById(currentFixture.HomeTeamId);
                player = playerService.GetRandomPlayer(homeTeam);
                matchService.PlayerAction(homeTeam, player, currentMatch);
            }
            else
            {
                var awayTeam = teamService.GetTeamById(currentFixture.AwayTeamId);
                player = playerService.GetRandomPlayer(awayTeam);
                matchService.PlayerAction(awayTeam, player, currentMatch);
            }

            var newModel = matchService.GetMatchModel(currentMatch, currentFixture, player);

            return View("Match", newModel);
        }
        public IActionResult Results(MatchViewModel mvm)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();
            var dayResults = matchService.GetResults(CurrentGame);          
            var round = dayResults.First().Round;

            return View(new MatchDayViewModel
            {
                DayFixtures = dayResults,
                Day = CurrentGame.Day,
                Year = CurrentGame.Year,
                Round = round,
                Leagues = leagueService.GetAllLeagues()
            });
        }
        public IActionResult ValidTactics(MatchViewModel mvm)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();
            (bool isValid, string error) = matchService.ValidateTactics(currentTeam);
            if (isValid)
            {
                return RedirectToAction("MatchPreview");
            }
            else
            {
                ModelState.AddModelError("", error);
                return RedirectToAction("Tactics");
            }

        }
        public IActionResult AddToStartingEleven(int id)
        {
            playerService.Substitution(id, "Add");
            return RedirectToAction("Tactics");
        }
        public IActionResult RemoveFromStartingEleven(int id)
        {
            playerService.Substitution(id, "Remove");
            return RedirectToAction("Tactics");
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
