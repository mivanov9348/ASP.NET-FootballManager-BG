namespace ASP.NET_FootballManager.Controllers
{
    using ASP.NET_FootballManager.Data.DataModels;
    using ASP.NET_FootballManager.Models;
    using ASP.NET_FootballManager.Models.Sorting;
    using ASP.NET_FootballManager.Services.Common;
    using ASP.NET_FootballManager.Services.Fixture;
    using ASP.NET_FootballManager.Services.Game;
    using ASP.NET_FootballManager.Services.Inbox;
    using ASP.NET_FootballManager.Services.League;
    using ASP.NET_FootballManager.Services.Manager;
    using ASP.NET_FootballManager.Services.Player;
    using ASP.NET_FootballManager.Services.Team;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Security.Claims;

    public class MenuController : Controller
    {
        private readonly ICommonService commonService;
        private readonly IManagerService managerService;
        private readonly ILeagueService leagueService;
        private readonly IGameService gameService;
        private readonly IPlayerService playerService;
        private readonly IFixtureService fixtureService;
        private readonly ITeamService teamService;
        private readonly IInboxService inboxService;
        private readonly IDayService dayService;
        public MenuController(IInboxService inboxService, ITeamService teamService, IFixtureService fixtureService, IPlayerService playerService, IGameService gameService, ICommonService commonService, ILeagueService leagueService, IManagerService managerService, IDayService dayService)
        {
            this.commonService = commonService;
            this.leagueService = leagueService;
            this.managerService = managerService;
            this.gameService = gameService;
            this.playerService = playerService;
            this.fixtureService = fixtureService;
            this.teamService = teamService;
            this.inboxService = inboxService;
            this.dayService = dayService;
        }
        public IActionResult Inbox(int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();
            var currentInboxMessage = inboxService.GetInboxMessages(CurrentGame.Id).OrderByDescending(x => x.Id).ToList();
            var currentMessage = inboxService.GetFullMessage(id, CurrentGame);

            return View(new InboxViewModel
            {
                News = currentInboxMessage,
                CurrentNews = currentMessage,
                Year = currentMessage.Year,
                Day = currentMessage.Day
            });
        }
        public IActionResult OpenNews(int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();
            var currentInboxMessage = inboxService.GetInboxMessages(CurrentGame.Id).OrderByDescending(x => x.Id).ToList();
            var currentMessage = inboxService.GetFullMessage(id, CurrentGame);

            return View("Inbox", new InboxViewModel
            {
                News = currentInboxMessage,
                CurrentNews = currentMessage
            });
        }
        public IActionResult NextMatch()
        {
            return View();
        }
        public IActionResult Fixtures(FixturesViewModel fvm)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();
            var allLeagues = leagueService.GetAllLeagues();
            var currentFixtures = fixtureService.GetFixture(fvm.LeagueId, fvm.CurrentRound, CurrentGame);
            var rounds = fixtureService.GetAllRounds(fvm.LeagueId);
            var currentLeague = leagueService.GetLeague(fvm.LeagueId);

            return View(new FixturesViewModel
            {
                Leagues = allLeagues,
                Fixtures = currentFixtures,
                AllRounds = rounds,
                LeagueId = fvm.LeagueId,
                CurrentLeagueName = currentLeague.Name.ToUpper()
            });
        }
        public IActionResult CupsFixture(FixturesViewModel fvm)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();
            var allLeagues = leagueService.GetAllLeagues();
            var currentFixtures = fixtureService.GetFixture(fvm.LeagueId, fvm.CurrentRound, CurrentGame);
            var rounds = fixtureService.GetAllRounds(fvm.LeagueId);
            var currentLeague = leagueService.GetLeague(fvm.LeagueId);

            return View(new FixturesViewModel
            {
                Leagues = allLeagues,
                Fixtures = currentFixtures,
                AllRounds = rounds,
                LeagueId = fvm.LeagueId,
                CurrentLeagueName = currentLeague.Name.ToUpper()
            });
        }
        public IActionResult ChooseRound(int id, FixturesViewModel fvm)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();
            var allLeagues = leagueService.GetAllLeagues();
            var currentFixtures = fixtureService.GetFixture(fvm.LeagueId, id, CurrentGame);
            var rounds = fixtureService.GetAllRounds(fvm.LeagueId);
            var currentLeague = leagueService.GetLeague(fvm.LeagueId);

            return View("Fixtures", new FixturesViewModel
            {
                Leagues = allLeagues,
                Fixtures = currentFixtures,
                AllRounds = rounds,
                LeagueId = fvm.LeagueId,
                CurrentLeagueName = currentLeague.Name.ToUpper()
            });
        }
        public IActionResult Calendar()
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();
            var days = dayService.GetAllDays(CurrentGame);
            var year = CurrentGame.Year;

            return View(new CalendarViewModel
            {
                Days = days,
                Year = year
            });
        }
        public IActionResult Standings(StandingsViewModel svm)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();

            if (svm.LeagueId == 0)
            {
                svm.Leagues = leagueService.GetAllLeagues();
                svm.VirtualTeams = leagueService.GetStandingsByLeague(svm.LeagueId, CurrentGame);
            }
            else
            {
                svm.Leagues = leagueService.GetAllLeagues();
                svm.VirtualTeams = leagueService.GetStandingsByLeague(svm.LeagueId, CurrentGame);
            }

            return View(svm);
        }
        public IActionResult PlayersStats(PlayersViewModel pvm, int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();
            pvm = playerService.SortingPlayers(pvm.PlayerSorting, id, CurrentGame);
            return View(pvm);
        }
        public IActionResult TeamStats()
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();
            var originalTeam = teamService.GetOriginalTeam(currentTeam, CurrentGame);

            return View(new TeamViewModel
            {
                Team = originalTeam,
                CurrentTeam = currentTeam,
                Players = playerService.GetPlayersByTeam(currentTeam.Id),
                Nations = commonService.GetAllNations(),
                Teams = teamService.GetAllVirtualTeams(CurrentGame),
                Cities = commonService.GetAllCities(),
                Positions = commonService.GetAllPositions(),
                Leagues = leagueService.GetAllLeagues()
            });
        }
        public IActionResult PlayerDetails(int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();
            var currentPlayer = playerService.GetPlayerById(id);
            var nation = commonService.GetAllNations().FirstOrDefault(x => x.Id == currentPlayer.NationId);
            var position = commonService.GetAllPositions().FirstOrDefault(x => x.Id == currentPlayer.PositionId);
            var team = teamService.GetAllVirtualTeams(CurrentGame).FirstOrDefault(x => x.Id == currentPlayer.TeamId);

            return View(new PlayersViewModel
            {
                FullName = currentPlayer.FirstName + " " + currentPlayer.LastName,
                Age = currentPlayer.Age,
                Attack = currentPlayer.Attack,
                Defense = currentPlayer.Defense,
                City = currentPlayer.Team.Name,
                Position = position.Name,
                ImageUrl = currentPlayer.ProfileImage,
                Goals = currentPlayer.Goals,
                Overall = currentPlayer.Overall,
                Nation = nation.Name,
                Team = team.Name

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
