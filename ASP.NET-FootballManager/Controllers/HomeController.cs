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
    using ASP.NET_FootballManager.Services.League;
    using ASP.NET_FootballManager.Services.Player;
    using System.Text;
    using ASP.NET_FootballManager.Services.Inbox;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICommonService commonService;
        private readonly IValidationService validationService;
        private readonly IManagerService managerService;
        private readonly IGameService gameService;
        private readonly ITeamService teamService;
        private readonly ILeagueService leagueService;
        private readonly IPlayerService playerService;
        private readonly IInboxService inboxService;
        private string UserId;
        public HomeController(ILogger<HomeController> logger,
            IGameService gameService,
            IManagerService managerService,
            ICommonService commonService,
            IValidationService validationService,
            ITeamService teamService,
            ILeagueService leagueService,
            IPlayerService playerService,
            IInboxService inboxService)
        {
            _logger = logger;
            this.managerService = managerService;
            this.commonService = commonService;
            this.validationService = validationService;
            this.gameService = gameService;
            this.teamService = teamService;
            this.leagueService = leagueService;
            this.playerService = playerService;
            this.inboxService = inboxService;
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
        public IActionResult NewGame()
        {
            CurrentUser();
            bool isExistGame = gameService.isExistGame(UserId);

            if (isExistGame)
            {
                return RedirectToAction("ExistingGame");
            }

            return View(new NewManagerViewModel
            {
                Nations = commonService.GetAllNations(),
                Teams = commonService.GetAllTeams()
            });
        }
        public IActionResult StartGame(NewManagerViewModel ngvm)
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
                var currentManager = managerService.CreateNewManager(ngvm, UserId);              
                var currentGame = gameService.CreateNewGame(currentManager);
                inboxService.CreateManagerNews(currentManager, currentGame);
                var teams = teamService.GenerateTeams(currentGame).Where(x => x.IsPlayable == true).ToList();
                teams.ForEach(x => playerService.GeneratePlayers(currentGame, x));
                teams.ForEach(x => teamService.CalculateTeamOverall(x));
                leagueService.GenerateFixtures(currentGame);
                playerService.CreateFreeAgents(currentGame, 30, 40, 40, 70);
                playerService.CalculatingPlayersPrice();
                return RedirectToAction("Inbox", "Menu");

            }
            else
            {
                ViewData["Error"] = ErrorMessage;
                return View("NewGame", new NewManagerViewModel
                {
                    Nations = commonService.GetAllNations(),
                    Teams = commonService.GetAllTeams()
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

        public IActionResult GameErrors()
        {
            return View();
        }
    }
}