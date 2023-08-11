namespace ASP.NET_FootballManager.Controllers
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.League;
    using FootballManager.Core.Models.Menu;
    using FootballManager.Core.Models.Player;
    using FootballManager.Core.Models.Team;
    using FootballManager.Core.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    public class MenuController : Controller
    {
        private readonly ServiceAggregator serviceAggregator;
        public MenuController(ServiceAggregator serviceAggregator)
        {
            this.serviceAggregator = serviceAggregator;
        }
        public async Task<IActionResult> Inbox(int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var currentInboxMessage = await serviceAggregator.inboxService.GetInboxMessages(CurrentGame.Id);
            var currentMessage = await serviceAggregator.inboxService.GetFullMessage(id, CurrentGame);

            return View(new InboxViewModel
            {
                News = currentInboxMessage.OrderByDescending(x => x.Id).ToList(),
                CurrentNews = currentMessage,
                Year = currentMessage.Year,
                Day = currentMessage.Day
            });
        }
        public async Task<IActionResult> OpenNews(int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var currentInboxMessage = await serviceAggregator.inboxService.GetInboxMessages(CurrentGame.Id);
            var currentMessage = await serviceAggregator.inboxService.GetFullMessage(id, CurrentGame);

            return View("Inbox", new InboxViewModel
            {
                News = currentInboxMessage.OrderByDescending(x => x.Id).ToList(),
                CurrentNews = currentMessage
            });
        }
        public IActionResult NextMatch()
        {
            return View();
        }
        public async Task<IActionResult> Fixtures(FixturesViewModel fvm)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var allLeagues = await serviceAggregator.leagueService.GetAllLeagues();
            var currentFixtures = await serviceAggregator.fixtureService.GetFixture(fvm.LeagueId, fvm.CurrentRound, CurrentGame);
            var rounds = await serviceAggregator.fixtureService.GetAllRounds(fvm.LeagueId);
            var currentLeague = await serviceAggregator.leagueService.GetLeague(fvm.LeagueId);

            return View(new FixturesViewModel
            {
                Leagues = allLeagues,
                Fixtures = currentFixtures,
                AllRounds = rounds,
                LeagueId = fvm.LeagueId,
                CurrentLeagueName = currentLeague.Name.ToUpper()
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

            return View("Fixtures", new FixturesViewModel
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

            return View("Fixtures", new FixturesViewModel
            {
                Leagues = allLeagues,
                Fixtures = currentFixtures,
                AllRounds = rounds,
                LeagueId = fvm.LeagueId,
                CurrentLeagueName = currentLeague.Name.ToUpper()
            });
        }
        public async Task<IActionResult> Calendar()
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var days = await serviceAggregator.dayService.GetAllDays(CurrentGame);
            var year = CurrentGame.Year;

            return View(new CalendarViewModel
            {
                Days = days,
                Year = year
            });
        }
        public async Task<IActionResult> Standings(StandingsViewModel svm)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) =    serviceAggregator.commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (svm.LeagueId == 0)
            {
                svm.Leagues = await serviceAggregator.leagueService.GetAllLeagues();
                svm.VirtualTeams = await serviceAggregator.leagueService.GetStandingsByLeague(svm.LeagueId, CurrentGame);
            }
            else
            {
                svm.Leagues = await serviceAggregator.leagueService.GetAllLeagues();
                svm.VirtualTeams = await serviceAggregator.leagueService.GetStandingsByLeague(svm.LeagueId, CurrentGame);
            }

            return View(svm);
        }
        public IActionResult PlayersStats(PlayersViewModel pvm, int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            pvm = serviceAggregator.playerSorterService.SortingPlayers(pvm.PlayerSorting, id, CurrentGame);
            return View(pvm);
        }
        public async Task<IActionResult> TeamStats()
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var originalTeam = await serviceAggregator.teamService.GetOriginalTeam(currentTeam, CurrentGame);

            return View(new TeamViewModel
            {
                Team = originalTeam,
                CurrentTeam = currentTeam,
                Players = await serviceAggregator.playerDataService.GetPlayersByTeam(currentTeam.Id),
                Nations = await serviceAggregator.commonService.GetAllNations(),
                Teams = await serviceAggregator.teamService.GetAllVirtualTeams(CurrentGame),
                Cities = await serviceAggregator.commonService.GetAllCities(),
                Positions = await serviceAggregator.commonService.GetAllPositions(),
                Leagues = await serviceAggregator.leagueService.GetAllLeagues(),
                PlayerAttributes = await serviceAggregator.commonService.GetAllPlayersAttribute(),
                PlayerStats = await serviceAggregator.commonService.GetAllPlayersStats()
            });
        }
        public async Task<IActionResult> PlayerDetails(int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var currentPlayer = await serviceAggregator.playerDataService.GetPlayerById(id);

            var model = this.serviceAggregator.playerModelService.PlayerDetailsViewModel(currentPlayer, CurrentGame);

            return View(model);
        }

    }
}
