namespace ASP.NET_FootballManager.Controllers
{
    using Data.Constant;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;  
    using FootballManager.Core.Models.Game;
    using FootballManager.Core.Services;
    using FootballManager.Infrastructure.Data.DataModels;

    public class GameController : Controller
    {
        private readonly ServiceAggregator _serviceAggregator;

        public GameController(ServiceAggregator serviceAggregator)
        {
            _serviceAggregator = serviceAggregator ?? throw new ArgumentNullException(nameof(serviceAggregator));
        }

        public async Task<IActionResult> SeasonStats(EndSeasonViewModel esvm)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var (userIdResult, currentManager, currentGame, currentTeam) = _serviceAggregator.gameService.CurrentGameInfo(userId);

            var model = await BuildEndSeasonViewModel(esvm, currentGame);
            return View(model);
        }

        public async Task<IActionResult> NewSeason()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var (userIdResult, currentManager, currentGame, currentTeam) = _serviceAggregator.gameService.CurrentGameInfo(userId);

            await ResetAndGenerateNewSeason(currentGame);
            return RedirectToAction("Index", "Menu");
        }

        public async Task<IActionResult> EndSeason(EndSeasonViewModel esvm)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var (userIdResult, currentManager, currentGame, currentTeam) = _serviceAggregator.gameService.CurrentGameInfo(userId);
            var teams = await _serviceAggregator.leagueService.GetStandingsByLeague(esvm.LeagueId, currentGame);

            return View(new EndSeasonViewModel
            {
                Leagues = await _serviceAggregator.leagueService.GetAllLeagues(),
                Teams = teams
            });
        }

        private async Task<EndSeasonViewModel> BuildEndSeasonViewModel(EndSeasonViewModel esvm, Game currentGame)
        {
            var goalScorer = await _serviceAggregator.playerDataService.GetLeagueGoalscorer(currentGame, esvm.LeagueId);
            var teams = await _serviceAggregator.leagueService.GetStandingsByLeague(esvm.LeagueId, currentGame);
            var league = await _serviceAggregator.leagueService.GetLeague(esvm.LeagueId);
            var euroCup = await _serviceAggregator.euroCupService.GetEuropeanCup(esvm.EuroCupId);
            var cup = _serviceAggregator.cupService.GetCurrentCup(currentGame);
            var cupWinner = await _serviceAggregator.cupService.GetWinner(currentGame);
            var championsCupWinner = await _serviceAggregator.euroCupService.GetChampionsCupWinner(currentGame);
            var euroCupWinner = await _serviceAggregator.euroCupService.GetEuroCupWinner(currentGame);

            return new EndSeasonViewModel
            {
                GoalScorer = goalScorer,
                Leagues = await _serviceAggregator.leagueService.GetAllLeagues(),
                EuroCups = await _serviceAggregator.euroCupService.AllEuroCups(),
                EuroCup = euroCup,
                Teams = teams,
                League = league,
                Cup = cup,
                CupWinner = cupWinner,
                EuroCupWinner = euroCupWinner,
                ChampionsCupWinner = championsCupWinner
            };
        }

        private async Task ResetAndGenerateNewSeason(Game currentGame)
        {
            await _serviceAggregator.leagueService.PromotedRelegated(currentGame);
            _serviceAggregator.gameService.ResetGame(currentGame);
            _serviceAggregator.fixtureService.AddLeagueFixtureToDay(currentGame);
            _serviceAggregator.matchService.DeleteMatches(currentGame);
            _serviceAggregator.fixtureService.DeleteFixtures(currentGame);
            _serviceAggregator.fixtureService.GenerateLeagueFixtures(currentGame);
            _serviceAggregator.cupService.GenerateCupParticipants(currentGame);

            _serviceAggregator.playerGeneratorService.CreateFreeAgents(
                currentGame,
                DataConstants.FreeAgentsEachClub.gk,
                DataConstants.FreeAgentsEachClub.df,
                DataConstants.FreeAgentsEachClub.mf,
                DataConstants.FreeAgentsEachClub.st);

            _serviceAggregator.playerStatsService.CalculatingPlayersPrice(currentGame);
            _serviceAggregator.playerStatsService.ResetPlayerStats(currentGame);
            _serviceAggregator.teamService.ResetTeams(currentGame);
            _serviceAggregator.inboxService.NewSeasonNews(currentGame);
        }
    }
}
