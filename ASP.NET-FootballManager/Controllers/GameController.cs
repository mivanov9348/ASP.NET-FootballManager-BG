namespace ASP.NET_FootballManager.Controllers
{
    using Data.Constant;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;  
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Game;
    using FootballManager.Core.Services;

    public class GameController : Controller
    {       
        private readonly ServiceAggregator serviceAggregator;
        public GameController(ServiceAggregator serviceAggregator)
        {
            this.serviceAggregator = serviceAggregator;            
        }
        public async Task<IActionResult> SeasonStats(EndSeasonViewModel esvm)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var goalScorer = await serviceAggregator.playerDataService.GetLeagueGoalscorer(CurrentGame, esvm.LeagueId);
            var teams = await serviceAggregator.leagueService.GetStandingsByLeague(esvm.LeagueId, CurrentGame);
            var league = await serviceAggregator.leagueService.GetLeague(esvm.LeagueId);
            var euroCup = await serviceAggregator.euroCupService.GetEuropeanCup(esvm.EuroCupId);
            var cup = await serviceAggregator.cupService.GetCurrentCup();
            var cupWinner = await serviceAggregator.cupService.GetWinner(CurrentGame);
            var championsCupWinner = await serviceAggregator.euroCupService.GetChampionsCupWinner(CurrentGame);
            var euroCupWinner = await serviceAggregator.euroCupService.GetEuroCupWinner(CurrentGame);

            return View(new EndSeasonViewModel
            {
                GoalScorer = goalScorer,
                Leagues = await serviceAggregator.leagueService.GetAllLeagues(),
                EuroCups = await serviceAggregator.euroCupService.AllEuroCups(),
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
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) =  serviceAggregator.commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            await serviceAggregator.leagueService.PromotedRelegated(CurrentGame);
            serviceAggregator.gameService.ResetGame(CurrentGame);
            //serviceAggregator.dayService.CalculateDays(CurrentGame);
            serviceAggregator.fixtureService.AddLeagueFixtureToDay(CurrentGame);
            serviceAggregator.matchService.DeleteMatches(CurrentGame);
            serviceAggregator.fixtureService.DeleteFixtures(CurrentGame);
            serviceAggregator.fixtureService.GenerateLeagueFixtures(CurrentGame);
            serviceAggregator.cupService.GenerateCupParticipants(CurrentGame);
      

            serviceAggregator.playerGeneratorService.CreateFreeAgents(CurrentGame, DataConstants.FreeAgentsEachClub.gk, DataConstants.FreeAgentsEachClub.df, DataConstants.FreeAgentsEachClub.mf, DataConstants.FreeAgentsEachClub.st);
            serviceAggregator.playerStatsService.CalculatingPlayersPrice(CurrentGame);
           // playerService.UpdateAttributes(CurrentGame);
            serviceAggregator.playerStatsService.ResetPlayerStats(CurrentGame);
            serviceAggregator.teamService.ResetTeams(CurrentGame);
            serviceAggregator.inboxService.NewSeasonNews(CurrentGame);

            return RedirectToAction("Inbox", "Menu");
        }
        public async Task<IActionResult> EndSeason(EndSeasonViewModel esvm)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var teams = await serviceAggregator.leagueService.GetStandingsByLeague(esvm.LeagueId, CurrentGame);

            return View(new EndSeasonViewModel
            {
                Leagues = await serviceAggregator.leagueService.GetAllLeagues(),
                Teams = teams
            });
        }        
    }
}
