namespace ASP.NET_FootballManager.Controllers
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels; 
    using FootballManager.Core.Models.Match;
    using FootballManager.Core.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    public class MatchController : Controller
    {
        private readonly ServiceAggregator serviceAggregator;
        public MatchController(ServiceAggregator serviceAggregator)
        {
            this.serviceAggregator = serviceAggregator;
        }
        public async Task<IActionResult> MatchDayPreview()
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var dayFixtures = await serviceAggregator.matchService.GetFixturesByDay(CurrentGame);

            if (dayFixtures == null || dayFixtures.Count == 0)
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
                Leagues = await serviceAggregator.leagueService.GetAllLeagues(),
                CurrentTeam = currentTeam
            });
        }
        public async Task<IActionResult> MatchPreview()
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var dayFixtures = await serviceAggregator.matchService.GetFixturesByDay(CurrentGame);
            var currentFixture = await serviceAggregator.matchService.GetCurrentFixture(dayFixtures, CurrentGame);
            var homeTeamPlayers = await     serviceAggregator.matchService.GetStarting11(currentFixture.HomeTeamId);
            var awayTeamPlayers = await serviceAggregator.matchService.GetStarting11(currentFixture.AwayTeamId);

            return View(new MatchViewModel
            {
                Positions = await serviceAggregator.commonService.GetAllPositions(),
                HomeTeamName = currentFixture.HomeTeamName,
                AwayTeamName = currentFixture.AwayTeamName,
                CurrentFixture = currentFixture,
                HomeTeamPlayers = homeTeamPlayers.OrderBy(x => x.PositionId).ToList(),
                AwayTeamPlayers = awayTeamPlayers.OrderBy(x => x.PositionId).ToList()
            });
        }
        public async Task<IActionResult> Tactics()
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var clubStartingEleven = await serviceAggregator.playerService.GetStartingEleven(currentTeam.Id);
            var clubSubstitutes = await serviceAggregator.playerService.GetSubstitutes(currentTeam.Id);
            var positions = await serviceAggregator.commonService.GetAllPositions();
            var dayFixtures = await serviceAggregator.matchService.GetFixturesByDay(CurrentGame);
            var currentFixture = await serviceAggregator.matchService.GetCurrentFixture(dayFixtures, CurrentGame);
            var currentDay = await serviceAggregator.dayService.GetCurrentDay(CurrentGame);

            if (currentFixture == null)
            {
                if (currentDay.isCupDay)
                {
                    serviceAggregator.cupService.CalculateOtherMatches(dayFixtures, currentFixture);
                    serviceAggregator.fixtureService.GenerateCupFixtures(CurrentGame);
                }
                if (currentDay.isEuroCupDay)
                {
                    serviceAggregator.euroCupService.CalculateOtherMatches(dayFixtures, currentFixture);
                    serviceAggregator.fixtureService.GenerateEuroFixtures(CurrentGame);
                }
                serviceAggregator.inboxService.CupMatchesInfo(dayFixtures, CurrentGame);
                serviceAggregator.gameService.NextDay(CurrentGame);
                return RedirectToAction("Results", "Match");
            }

            return View(new TacticsViewModel
            {
                CurrentTeam = currentTeam,
                Substitutes = clubSubstitutes.OrderBy(x => x.PositionId).ToList(),
                StartingEleven = clubStartingEleven.OrderBy(x => x.PositionId).ToList(),
                Positions = positions
            });
        }
        public async Task<IActionResult> Match(MatchViewModel mvm)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var dayFixtures = await serviceAggregator.matchService.GetFixturesByDay(CurrentGame);
            var currentFixture = await serviceAggregator.matchService.GetCurrentFixture(dayFixtures, CurrentGame);
            var homeTeamPlayers = await serviceAggregator.matchService.GetStarting11(currentFixture.HomeTeamId);
            var currentMatch = serviceAggregator.matchService.CreateMatch(currentFixture, CurrentGame);
            var newModel = await serviceAggregator.matchService.GetMatchModel(currentMatch, currentFixture, homeTeamPlayers.OrderBy(x => x.PositionId).First());
            return View(newModel);
        }
        public async Task<IActionResult> GetAction(int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var currentMatch = await serviceAggregator.matchService.GetCurrentMatch(id);
            var dayFixtures = await serviceAggregator.matchService.GetFixturesByDay(CurrentGame);
            var currentFixture = await serviceAggregator.matchService.GetCurrentFixture(dayFixtures, CurrentGame);
            var currentDay = await serviceAggregator.dayService.GetCurrentDay(CurrentGame);
            var player = new Player();

            serviceAggregator.matchService.Time(currentMatch);
            if (currentMatch.Minute > 90)
            {
                serviceAggregator.matchService.EndMatch(currentMatch);
                if (currentDay.isLeagueDay)
                {
                    serviceAggregator.leagueService.CheckWinner(currentFixture.HomeTeamGoal, currentFixture.AwayTeamGoal, currentFixture);
                    serviceAggregator.leagueService.CalculateOtherMatches(dayFixtures, currentFixture);
                }
                if (currentDay.isCupDay)
                {
                    serviceAggregator.cupService.CheckWinner(currentFixture);
                    serviceAggregator.cupService.CalculateOtherMatches(dayFixtures, currentFixture);
                    serviceAggregator.fixtureService.GenerateCupFixtures(CurrentGame);
                }
                if (currentDay.isEuroCupDay)
                {
                    serviceAggregator.euroCupService.CheckWinner(currentFixture);
                    serviceAggregator.euroCupService.CalculateOtherMatches(dayFixtures, currentFixture);
                    serviceAggregator.fixtureService.GenerateEuroFixtures(CurrentGame);
                }
                serviceAggregator.gameService.NextDay(CurrentGame);
                serviceAggregator.inboxService.MatchFinishedNews(CurrentGame, currentFixture);
                return RedirectToAction("Results");
            }

            if (currentMatch.Turn == 1)
            {
                var homeTeam = await serviceAggregator.teamService.GetTeamById(currentFixture.HomeTeamId);
                player = await serviceAggregator.playerService.GetRandomPlayer(homeTeam);
                serviceAggregator.matchService.PlayerAction(homeTeam, player, currentMatch);
            }
            else
            {
                var awayTeam = await serviceAggregator.teamService.GetTeamById(currentFixture.AwayTeamId);
                player = await serviceAggregator.playerService.GetRandomPlayer(awayTeam);
                serviceAggregator.matchService.PlayerAction(awayTeam, player, currentMatch);
            }

            var newModel = await serviceAggregator.matchService.GetMatchModel(currentMatch, currentFixture, player);

            return View("Match", newModel);
        }
        public async Task<IActionResult> Results(MatchViewModel mvm)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var dayResults = await serviceAggregator.matchService.GetResults(CurrentGame);
            var round = dayResults.First().Round;

            return View(new MatchDayViewModel
            {
                DayFixtures = dayResults,
                Day = CurrentGame.Day,
                Year = CurrentGame.Year,
                Round = round,
                Leagues = await serviceAggregator.leagueService.GetAllLeagues()
            });
        }
        public IActionResult ValidTactics(MatchViewModel mvm)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            (bool isValid, string error) = serviceAggregator.matchService.ValidateTactics(currentTeam);
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
            serviceAggregator.playerService.Substitution(id, "Add");
            return RedirectToAction("Tactics");
        }
        public IActionResult RemoveFromStartingEleven(int id)
        {
            serviceAggregator.playerService.Substitution(id, "Remove");
            return RedirectToAction("Tactics");
        }

    }
}
