using ASP.NET_FootballManager.Data;
using ASP.NET_FootballManager.Models;
using ASP.NET_FootballManager.Services.Common;
namespace ASP.NET_FootballManager.Controllers
{
    using ASP.NET_FootballManager.Services.Manager;
    using ASP.NET_FootballManager.Services.Validation;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Security.Claims;
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICommonService commonService;
        private readonly IValidationService validationService;
        private readonly IManagerService managerService;
        private string UserId;

        public HomeController(ILogger<HomeController> logger, IManagerService managerService, ICommonService commonService, IValidationService validationService)
        {
            _logger = logger;
            this.managerService = managerService;
            this.commonService = commonService;
            this.validationService = validationService;
        }

        public IActionResult Index()
        {

            return View();

        }

        public IActionResult NewGame()
        {
            CurrentUser();
            var currentManager = commonService.GetCurrentManager(UserId);

            if (currentManager == null)
            {
                return View(new NewGameViewModel
                {
                    Nations = commonService.GetAllNations(),
                    Teams = commonService.GetAllTeams()
                });
            }
            return View("ExistingGame");

        }

        [HttpPost]
        public IActionResult StartGame(NewGameViewModel ngvm)
        {
            CurrentUser();
            (bool isValid, string ErrorMessage) = validationService.NewManagerValidator(ngvm);

            if (isValid)
            {
                managerService.CreateNewManager(ngvm, UserId);
                return RedirectToAction("Menu", "MainMenu");
            }

            return View();

        }


        private void CurrentUser()
        {
            UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}