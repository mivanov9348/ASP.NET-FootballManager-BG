namespace ASP.NET_FootballManager.Controllers
{
    using ASP.NET_FootballManager.Services.Game;
    using ASP.NET_FootballManager.Services.Manager;
    using ASP.NET_FootballManager.Services.Validation;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Security.Claims;
    using ASP.NET_FootballManager.Models;
    using ASP.NET_FootballManager.Services.Common;
    using ASP.NET_FootballManager.Services.Team;
    using ASP.NET_FootballManager.Services.Player;
    using ASP.NET_FootballManager.Services.Inbox;
    using ASP.NET_FootballManager.Services.Fixture;
    using ASP.NET_FootballManager.Services.EuroCup;
    using ASP.NET_FootballManager.Services.Cup;
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICommonService commonService;
        private readonly IValidationService validationService;
        private readonly IManagerService managerService;
        private readonly IGameService gameService;
        private readonly ITeamService teamService;
        private readonly IPlayerService playerService;
        private readonly IInboxService inboxService;
        private readonly IFixtureService fixtureService;
        private readonly IDayService dayService;
        private readonly IEuroCupService euroCupService;
        private readonly ICupService cupService;
        private string UserId;       
        public HomeController(ILogger<HomeController> logger,
            IGameService gameService,
            IManagerService managerService,
            ICommonService commonService,
            IValidationService validationService,
            ITeamService teamService,
            IPlayerService playerService,
            IInboxService inboxService,
            IFixtureService fixtureService,
            IDayService dayService,
            IEuroCupService euroCupService,
            ICupService cupService)
        {
            _logger = logger;
            this.managerService = managerService;
            this.commonService = commonService;
            this.validationService = validationService;
            this.gameService = gameService;
            this.teamService = teamService;
            this.playerService = playerService;
            this.inboxService = inboxService;
            this.fixtureService = fixtureService;
            this.dayService = dayService;
            this.euroCupService = euroCupService;
            this.cupService = cupService;           
        }
        public IActionResult Index()
        {
            CurrentUser();

            if (UserId == null)
            {
                return View();
            }

            bool isExistGame = gameService.isExistGame(UserId);
            if (isExistGame)
            {
                return View(new GameViewModel { ExistGame = true });
            }
            return View(new GameViewModel { ExistGame = false });
        }
        public async Task<IActionResult> NewGame(int id)
        {
            CurrentUser();
            bool isExistGame = gameService.isExistGame(UserId);

            if (isExistGame)
            {
                if (id == 1)
                {
                    gameService.ResetSave(UserId);
                    return View(new NewManagerViewModel
                    {
                        Nations = await commonService.GetAllNations(),
                        Teams = await teamService.GetAllPlayableTeams()
                    });
                }
                return RedirectToAction("ExistingGame");
            }

            return View(new NewManagerViewModel
            {
                Nations = await commonService.GetAllNations(),
                Teams = await teamService.GetAllPlayableTeams()
            });
        }
        public async Task<IActionResult> StartGame(NewManagerViewModel ngvm)
        {
            CurrentUser();
            bool isExistGame = gameService.isExistGame(UserId);

            if (isExistGame)
            {
                managerService.DeleteCurrentManager(UserId);
            }

            (bool isValid, string ErrorMessage) = validationService.NewManagerValidator(ngvm);

            if (isValid)
            {              
                //CreateManager               
                var currentManager = managerService.CreateNewManager(ngvm, UserId);
                var currentGame = gameService.CreateNewGame(currentManager);
                //CalculateDaysForSeason              
                dayService.CalculateDays(currentGame);
                fixtureService.AddFixtureToDay(currentGame);
                //NewManagerNews                
                inboxService.CreateManagerNews(currentManager, currentGame);
                //GenerateTeamsForGame             
                var teams = teamService.GenerateTeams(currentGame).ToList();
                //GenerateCup               
                cupService.GenerateCupParticipants(currentGame);
                fixtureService.GenerateCupFixtures(currentGame);
                //GenerateEuroCup
                euroCupService.DistributionEuroParticipant(currentGame);
                fixtureService.GenerateEuroFixtures(currentGame);
                //GeneratePlayersAndTeamOverall
                teams.ForEach(x => playerService.GeneratePlayers(currentGame, x));
                playerService.CreateFreeAgents(currentGame, 30, 40, 40, 70);
                playerService.CalculatingPlayersPrice(currentGame);
                 teams.ForEach(x => teamService.CalculateTeamOverall(x));
                //GenerateLeagueFixtures
                fixtureService.GenerateLeagueFixtures(currentGame);
                return RedirectToAction("Inbox", "Menu");
            }
            else
            {
                ViewData["Error"] = ErrorMessage;
                return View("NewGame", new NewManagerViewModel
                {
                    Nations = await commonService.GetAllNations(),
                    Teams = await teamService.GetAllTeams()
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
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }    
    }
}