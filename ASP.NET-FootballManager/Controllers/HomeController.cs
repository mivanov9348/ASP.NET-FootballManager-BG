namespace ASP.NET_FootballManager.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Security.Claims;
    using ASP.NET_FootballManager.Models;
    using FootballManager.Core.Services;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ServiceAggregator serviceAggregator;
        private string userId;
        public HomeController(ILogger<HomeController> logger,
            ServiceAggregator serviceAggregator)
        {
            _logger = logger;
            this.serviceAggregator = serviceAggregator;
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
            serviceAggregator.managerService.DeleteCurrentManager(userId);
            serviceAggregator.gameService.isExistGame(userId);
            bool isExistGame = serviceAggregator.gameService.isExistGame(userId);

            if (isExistGame)
            {
                if (id == 1)
                {
                    serviceAggregator.gameService.ResetSave(userId);
                    return View(new NewManagerViewModel
                    {
                        Nations = await serviceAggregator.commonService.GetAllNations(),
                        Teams = await serviceAggregator.teamService.GetAllPlayableTeams()
                    });
                }
                return RedirectToAction("ExistingGame");
            }

            return View(new NewManagerViewModel
            {
                Nations = await serviceAggregator.commonService.GetAllNations(),
                Teams = await serviceAggregator.teamService.GetAllPlayableTeams()
            });
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

        public async Task<IActionResult> AddImage(NewManagerViewModel model)
        {
            CurrentUser();
            var currentManager = serviceAggregator.managerService.GetCurrentManager(userId);
            serviceAggregator.managerService.AddImageToManager(model, userId);
            return View("StartGame");
        }

        public async Task<IActionResult> StartGame(NewManagerViewModel ngvm)
        {
            CurrentUser();
            bool isExistGame = serviceAggregator.gameService.isExistGame(userId);

            if (isExistGame)
            {
                serviceAggregator.managerService.DeleteCurrentManager(userId);
            }

            (bool isValid, string ErrorMessage) = serviceAggregator.validationService.NewManagerValidator(ngvm);

            if (isValid)
            {
                //CreateOptions
                var optionsExist = serviceAggregator.gameOptionsService.IsOptionsSet(userId);
                if (!optionsExist)
                {
                    serviceAggregator.gameOptionsService.SaveSampleOptions(userId);
                }
                //GetCurrentManager
                var currentManager = serviceAggregator.managerService.GetCurrentManager(userId);
                //CreateGame
                var currentGame = serviceAggregator.gameService.CreateNewGame(currentManager);
                //CalculateDaysForSeason              
                serviceAggregator.dayService.CalculateDays(currentGame);
                serviceAggregator.fixtureService.AddFixtureToDay(currentGame);
                //NewManagerNews                
                serviceAggregator.inboxService.CreateManagerNews(currentManager, currentGame);
                //GenerateTeamsForGame             
                var teams = serviceAggregator.teamService.GenerateTeams(currentGame).ToList();
                //GenerateCup               
                serviceAggregator.cupService.GenerateCupParticipants(currentGame);
                serviceAggregator.fixtureService.GenerateCupFixtures(currentGame);
                //GenerateEuroCup
                serviceAggregator.euroCupService.DistributionEuroParticipant(currentGame);
                serviceAggregator.fixtureService.GenerateEuroFixtures(currentGame);
                //GeneratePlayersAndTeamOverall
                teams.ForEach(x => serviceAggregator.playerGeneratorService.GeneratePlayers(currentGame, x));
                serviceAggregator.playerGeneratorService.CreateFreeAgents(currentGame, 30, 40, 40, 70);
                serviceAggregator.playerStatsService.CalculatingPlayersPrice(currentGame);
                teams.ForEach(x => serviceAggregator.teamService.CalculateTeamOverall(x));
                //GenerateLeagueFixtures
                serviceAggregator.fixtureService.GenerateLeagueFixtures(currentGame);
                return RedirectToAction("Index", "Inbox");
            }
            else
            {
                ViewData["Error"] = ErrorMessage;
                return View("NewGame", new NewManagerViewModel
                {
                    Nations = await serviceAggregator.commonService.GetAllNations(),
                    Teams = await serviceAggregator.teamService.GetAllTeams()
                });
            }
        }
        public IActionResult ExistingGame()
        {
            return View();
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