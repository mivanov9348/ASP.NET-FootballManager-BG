﻿namespace ASP.NET_FootballManager.Controllers
{
    using ASP.NET_FootballManager.Data.DataModels;
    using ASP.NET_FootballManager.Models;
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
        public MenuController(IInboxService inboxService, ITeamService teamService, IFixtureService fixtureService, IPlayerService playerService, IGameService gameService, ICommonService commonService, ILeagueService leagueService, IManagerService managerService)
        {
            this.commonService = commonService;
            this.leagueService = leagueService;
            this.managerService = managerService;
            this.gameService = gameService;
            this.playerService = playerService;
            this.fixtureService = fixtureService;
            this.teamService = teamService;
            this.inboxService = inboxService;
        }
        public IActionResult Inbox(int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();
            var currentInboxMessage = inboxService.GetInboxMessages(CurrentGame.Id).OrderByDescending(x => x.Id).ToList();
            var currentMessage = inboxService.GetFullMessage(id);

            return View(new InboxViewModel
            {
                News = currentInboxMessage,
                CurrentNews = currentMessage
            });
        }
        public IActionResult OpenNews(int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();
            var currentInboxMessage = inboxService.GetInboxMessages(CurrentGame.Id).OrderByDescending(x => x.Id).ToList();
            var currentMessage = inboxService.GetFullMessage(id);

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
            var allLeagues = leagueService.GetAllLeagues();
            var currentFixtures = fixtureService.GetFixture(fvm.LeagueId, fvm.CurrentRound);
            var rounds = fixtureService.GetAllRounds(fvm.LeagueId);
            return View(new FixturesViewModel
            {
                Leagues = allLeagues,
                Fixtures = currentFixtures,
                AllRounds = rounds,
                LeagueId = fvm.LeagueId
            });
        }
        public IActionResult ChooseRound(int id, FixturesViewModel fvm)
        {
            var allLeagues = leagueService.GetAllLeagues();
            var currentFixtures = fixtureService.GetFixture(fvm.LeagueId, id);
            var rounds = fixtureService.GetAllRounds(fvm.LeagueId);
            return View("Fixtures", new FixturesViewModel
            {
                Leagues = allLeagues,
                Fixtures = currentFixtures,
                AllRounds = rounds,
                LeagueId = fvm.LeagueId
            });
        }
        public IActionResult Standings(StandingsViewModel svm)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();

            if (svm.LeagueId == 0)
            {
                svm.Leagues = leagueService.GetAllLeagues();
                svm.VirtualTeams = leagueService.GetStandingsByLeague(svm.LeagueId);
            }
            else
            {
                svm.Leagues = leagueService.GetAllLeagues();
                svm.VirtualTeams = leagueService.GetStandingsByLeague(svm.LeagueId);
            }

            return View(svm);
        }
        public IActionResult PlayersStats(PlayersViewModel pvm)
        {
            pvm = playerService.SortingPlayers(pvm.SortBy);
            return View(pvm);
        }
        public IActionResult TeamStats()
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();
            var originalTeam = teamService.GetOriginalTeam(currentTeam);

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
            var path = "~/Images/Faces/1.png";

            return View(new PlayersViewModel
            {
                FullName = currentPlayer.FirstName + " " + currentPlayer.LastName,
                Age = currentPlayer.Age,
                Attack = currentPlayer.Attack,
                Defense = currentPlayer.Defense,
                City = currentPlayer.Team.Name,
                Position = position.Name,
                ImageUrl = path,
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
