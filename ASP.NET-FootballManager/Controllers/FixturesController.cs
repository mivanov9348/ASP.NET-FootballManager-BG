namespace ASP.NET_FootballManager.Controllers
{
    using FootballManager.Core.Models.League;
    using FootballManager.Core.Services;
    using FootballManager.Infrastructure.Data.DataModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    public class FixturesController : Controller
    {
        private readonly ServiceAggregator serviceAggregator;
        public FixturesController(ServiceAggregator serviceAggregator)
        {
            this.serviceAggregator = serviceAggregator;
        }

        public async Task<IActionResult> Index(FixturesViewModel fvm)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.gameService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var allLeagues = await serviceAggregator.leagueService.GetAllLeagues();
            var currentFixtures = await serviceAggregator.fixtureService.GetFixture(fvm.LeagueId, fvm.CurrentRound, CurrentGame);
            var rounds = await serviceAggregator.fixtureService.GetAllRounds(fvm.LeagueId);
            var currentLeague = await serviceAggregator.leagueService.GetLeague(fvm.LeagueId);
            var menuModel = serviceAggregator.modelService.GetMenuViewModel(CurrentGame);

            return View(new FixturesViewModel
            {
                Leagues = allLeagues,
                Fixtures = currentFixtures,
                AllRounds = rounds,
                LeagueId = fvm.LeagueId,
                CurrentLeagueName = currentLeague.Name.ToUpper(),
                MenuViewModel = menuModel
            });
        }
        public async Task<IActionResult> CupsFixture(FixturesViewModel fvm, int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var allLeagues = await serviceAggregator.leagueService.GetAllLeagues();
            var currentFixtures = new List<Fixture>();
            string CupName = "";
            var rounds = 1;

            switch (id)
            {
                case 1:
                    currentFixtures = await serviceAggregator.cupService.GetCupFixtures(CurrentGame);
                    CupName = "Cup";
                    break;
                case 2:
                    currentFixtures = await serviceAggregator.euroCupService.GetEuroCupFixtures(CurrentGame, 1);
                    CupName = "Champions Cup";
                    break;
                case 3:
                    currentFixtures = await serviceAggregator.euroCupService.GetEuroCupFixtures(CurrentGame, 2);
                    CupName = "Champions Cup";
                    break;
            }

            return View("Index", new FixturesViewModel
            {
                Leagues = allLeagues,
                Fixtures = currentFixtures,
                AllRounds = rounds,
                LeagueId = fvm.LeagueId,
                CurrentLeagueName = CupName.ToUpper()
            });
        }
        public async Task<IActionResult> ChooseRound(int id, FixturesViewModel fvm)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var allLeagues = await serviceAggregator.leagueService.GetAllLeagues();
            var currentFixtures = await serviceAggregator.fixtureService.GetFixture(fvm.LeagueId, id, CurrentGame);
            var rounds = await serviceAggregator.fixtureService.GetAllRounds(fvm.LeagueId);
            var currentLeague = await serviceAggregator.leagueService.GetLeague(fvm.LeagueId);

            return View("Index", new FixturesViewModel
            {
                Leagues = allLeagues,
                Fixtures = currentFixtures,
                AllRounds = rounds,
                LeagueId = fvm.LeagueId,
                CurrentLeagueName = currentLeague.Name.ToUpper()
            });
        }

    }
}
