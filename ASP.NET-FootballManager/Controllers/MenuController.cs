namespace ASP.NET_FootballManager.Controllers
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using ASP.NET_FootballManager.Services.Common;
    using ASP.NET_FootballManager.Services.Cup;
    using ASP.NET_FootballManager.Services.EuroCup;
    using ASP.NET_FootballManager.Services.Fixture;
    using ASP.NET_FootballManager.Services.Inbox;
    using ASP.NET_FootballManager.Services.League;
    using ASP.NET_FootballManager.Services.Player;
    using ASP.NET_FootballManager.Services.Team;
    using FootballManager.Core.Models.League;
    using FootballManager.Core.Models.Menu;
    using FootballManager.Core.Models.Player;
    using FootballManager.Core.Models.Team;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    public class MenuController : Controller
    {
        private readonly ICommonService commonService;
        private readonly ILeagueService leagueService;
        private readonly ICupService cupService;
        private readonly IEuroCupService euroCupService;
        private readonly IPlayerService playerService;
        private readonly IFixtureService fixtureService;
        private readonly ITeamService teamService;
        private readonly IInboxService inboxService;
        private readonly IDayService dayService;
        public MenuController(ICupService cupService, IEuroCupService euroCupService, IInboxService inboxService, ITeamService teamService, IFixtureService fixtureService, IPlayerService playerService, ICommonService commonService, ILeagueService leagueService, IDayService dayService)
        {
            this.commonService = commonService;
            this.leagueService = leagueService;
            this.playerService = playerService;
            this.fixtureService = fixtureService;
            this.teamService = teamService;
            this.inboxService = inboxService;
            this.dayService = dayService;
            this.cupService = cupService;
            this.euroCupService = euroCupService;
        }
        public async Task<IActionResult> Inbox(int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var currentInboxMessage = await inboxService.GetInboxMessages(CurrentGame.Id);
            var currentMessage = await inboxService.GetFullMessage(id, CurrentGame);

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
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var currentInboxMessage = await inboxService.GetInboxMessages(CurrentGame.Id);
            var currentMessage = await inboxService.GetFullMessage(id, CurrentGame);

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
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var allLeagues = await leagueService.GetAllLeagues();
            var currentFixtures = await fixtureService.GetFixture(fvm.LeagueId, fvm.CurrentRound, CurrentGame);
            var rounds = await fixtureService.GetAllRounds(fvm.LeagueId);
            var currentLeague = await leagueService.GetLeague(fvm.LeagueId);

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
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var allLeagues = await leagueService.GetAllLeagues();
            var currentFixtures = new List<Fixture>();
            string CupName = "";
            var rounds = 1;

            switch (id)
            {
                case 1:
                    currentFixtures = await cupService.GetCupFixtures(CurrentGame);
                    CupName = "Cup";
                    break;
                case 2:
                    currentFixtures = await euroCupService.GetEuroCupFixtures(CurrentGame, 1);
                    CupName = "Champions Cup";
                    break;
                case 3:
                    currentFixtures = await euroCupService.GetEuroCupFixtures(CurrentGame, 2);
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
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var allLeagues = await leagueService.GetAllLeagues();
            var currentFixtures = await fixtureService.GetFixture(fvm.LeagueId, id, CurrentGame);
            var rounds = await fixtureService.GetAllRounds(fvm.LeagueId);
            var currentLeague = await leagueService.GetLeague(fvm.LeagueId);

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
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var days = await dayService.GetAllDays(CurrentGame);
            var year = CurrentGame.Year;

            return View(new CalendarViewModel
            {
                Days = days,
                Year = year
            });
        }
        public async Task<IActionResult> Standings(StandingsViewModel svm)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (svm.LeagueId == 0)
            {
                svm.Leagues = await leagueService.GetAllLeagues();
                svm.VirtualTeams = await leagueService.GetStandingsByLeague(svm.LeagueId, CurrentGame);
            }
            else
            {
                svm.Leagues = await leagueService.GetAllLeagues();
                svm.VirtualTeams = await leagueService.GetStandingsByLeague(svm.LeagueId, CurrentGame);
            }

            return View(svm);
        }
        public IActionResult PlayersStats(PlayersViewModel pvm, int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            pvm = playerService.SortingPlayers(pvm.PlayerSorting, id, CurrentGame);
            return View(pvm);
        }
        public async Task<IActionResult> TeamStats()
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var originalTeam = await teamService.GetOriginalTeam(currentTeam, CurrentGame);

            return View(new TeamViewModel
            {
                Team = originalTeam,
                CurrentTeam = currentTeam,
                Players = await playerService.GetPlayersByTeam(currentTeam.Id),
                Nations = await commonService.GetAllNations(),
                Teams = await teamService.GetAllVirtualTeams(CurrentGame),
                Cities = await commonService.GetAllCities(),
                Positions = await commonService.GetAllPositions(),
                Leagues = await leagueService.GetAllLeagues(),
                Attributes = await commonService.GetAllPlayersAttribute()
            });
        }
        public async Task<IActionResult> PlayerDetails(int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var currentPlayer = await playerService.GetPlayerById(id);

            var model = this.playerService.PlayerDetailsViewModel(currentPlayer, CurrentGame);

            return View(model);
        }
    }
}
