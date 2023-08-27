namespace ASP.NET_FootballManager.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Security.Claims;
    using ASP.NET_FootballManager.Models;
    using FootballManager.Core.Services;
    using FootballManager.Core.Models.Home;
    using System.Linq;
    using Microsoft.AspNetCore.SignalR;
    using ASP.NET_FootballManager.Hub;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ServiceAggregator serviceAggregator;
        private readonly IHubContext<GameHub> hubContext;
        private string userId;
        public HomeController(ILogger<HomeController> logger,
            ServiceAggregator serviceAggregator, IHubContext<GameHub> hubContext)
        {
            _logger = logger;
            this.serviceAggregator = serviceAggregator;
            this.hubContext = hubContext;
        }
        public IActionResult Index()
        {
            CurrentUser();

            if (userId == null)
            {
                return View();
            }

            bool isExistGame = serviceAggregator.gameService.isExistGame(userId);
            if (isExistGame)
            {
                return View(new GameViewModel { ExistGame = true });
            }
            return View(new GameViewModel { ExistGame = false });
        }
        public async Task<IActionResult> NewGame(int id)
        {
            CurrentUser();
            serviceAggregator.gameService.isExistGame(userId);
            bool isExistGame = serviceAggregator.gameService.isExistGame(userId);

            if (isExistGame)
            {
                return RedirectToAction("ExistingGame");
            }

            return View(new NewManagerViewModel
            {
                Nations = await serviceAggregator.commonService.GetAllNations(),
                Teams = await serviceAggregator.teamService.GetAllPlayableTeams()
            });
        }

        public async Task<IActionResult> ExistingGame(int id)
        {
            if (id == 1)
            {
                CurrentUser();
                serviceAggregator.gameService.ResetSave(userId);
                //serviceAggregator.managerService.DeleteCurrentManager(userId);
                return View("NewGame", new NewManagerViewModel
                {
                    Teams = await this.serviceAggregator.teamService.GetAllPlayableTeams()
                });
            }
            return View();
        }

        public async Task<IActionResult> SelectImage(NewManagerViewModel model)
        {
            CurrentUser();
            var currentManager = serviceAggregator.managerService.CreateNewManager(model, userId);

            return View(new NewManagerViewModel
            {
                Teams = await serviceAggregator.teamService.GetAllPlayableTeams()
            });
        }

        public async Task<IActionResult> AddImage(NewManagerViewModel model, int imageId)
        {
            CurrentUser();
            var currentManager = serviceAggregator.managerService.GetCurrentManager(userId);
            serviceAggregator.managerService.AddImageToManager(model, userId);
            return RedirectToAction("StartingGame");
        }
        public async Task<ActionResult> StartingGame(int id)
        {
            CurrentUser();

            var currentManager = serviceAggregator.managerService.GetCurrentManager(userId);
            var team = serviceAggregator.teamService.GetManagerTeam(currentManager);

            if (id == 0)
            {
                return View(new StartingGameViewModel
                {
                    ManagerName = $"{currentManager.FirstName} {currentManager.LastName}",
                    ManagerImage = currentManager.ImageId,
                    ManagerTeam = team.Name,
                    ManagerTeamImage = team.ImageUrl
                }); ;
            }
            else
            {
                await StartGame();
                return RedirectToAction("Index", "Inbox");
            }
        }
        private async Task SendMessageToHTML(string message)
        {
            await hubContext.Clients.All.SendAsync("ReceiveMessage", message);
        }
        private async Task StartGame()
        {
            CurrentUser();

            //CreateOptions
            var optionsExist = serviceAggregator.gameOptionsService.IsOptionsSet(userId);
            if (!optionsExist)
            {
                serviceAggregator.gameOptionsService.SaveSampleOptions(userId);
                await SendMessageToHTML("Options have been created.");
            }
            //GetCurrentManager
            var currentManager = serviceAggregator.managerService.GetCurrentManager(userId);
            //CreateGame
            await SendMessageToHTML("Creating New Game...");
            var currentGame = serviceAggregator.gameService.CreateNewGame(currentManager, userId);
            //CalculateDaysForSeason              
            await SendMessageToHTML("Calculating Days...");
            serviceAggregator.dayService.CalculateDays(currentGame);
            serviceAggregator.fixtureService.AddFixtureToDay(currentGame);
            //NewManagerNews
            await SendMessageToHTML("Writing news...");
            serviceAggregator.inboxService.CreateManagerNews(currentManager, currentGame);
            //GenerateTeamsForGame
            await SendMessageToHTML("Generating teams...");
            var teams = serviceAggregator.teamService.GenerateTeams(currentGame).ToList();
            //GenerateCup               
            await SendMessageToHTML("Generating cups...");
            serviceAggregator.cupService.GenerateCupParticipants(currentGame);
            serviceAggregator.fixtureService.GenerateCupFixtures(currentGame);
            //GenerateEuroCup
            await SendMessageToHTML("Generating european cups...");
            serviceAggregator.euroCupService.DistributionEuroParticipant(currentGame);
            serviceAggregator.fixtureService.GenerateEuroFixtures(currentGame);
            //GeneratePlayersAndTeamOverall
            await SendMessageToHTML("Generating players...");
            teams.ForEach(x => serviceAggregator.playerGeneratorService.GeneratePlayers(currentGame, x));
            await SendMessageToHTML("Generating free agents...");
            serviceAggregator.playerGeneratorService.CreateFreeAgents(currentGame, 30, 40, 40, 70);
            await SendMessageToHTML("Calculating players prices...");
            serviceAggregator.playerStatsService.CalculatingPlayersPrice(currentGame);
            await SendMessageToHTML("Calculating players overall...");
            teams.ForEach(x => serviceAggregator.teamService.CalculateTeamOverall(x));
            //GenerateLeagueFixtures
            await SendMessageToHTML("Generating fixtures and starting the game...");
            serviceAggregator.fixtureService.GenerateLeagueFixtures(currentGame);
        }

        private void CurrentUser()
        {
            if (this.User.Identity.IsAuthenticated != false)
            {
                userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}