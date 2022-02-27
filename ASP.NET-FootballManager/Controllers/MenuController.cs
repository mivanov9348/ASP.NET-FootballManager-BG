namespace ASP.NET_FootballManager.Controllers
{
    using ASP.NET_FootballManager.Models;
    using ASP.NET_FootballManager.Services.Common;
    using Microsoft.AspNetCore.Mvc;

    public class MenuController : Controller
    {
        private readonly ICommonService commonService;
        private readonly int currentManagerId;
        public MenuController(ICommonService commonService)
        {
            this.commonService = commonService;
        }

        public IActionResult Inbox(int id)
        {

            var currentGame = commonService.GetCurrentGame(id);
            var currentInboxMessage = commonService.GetInboxMessages(currentGame.Id);

            return View(new InboxViewModel
            {
                News = currentInboxMessage.OrderByDescending(x=>x.Id).ToList()
            });
        }
        public IActionResult NextMatch()
        {
            return View();
        }
        public IActionResult Standings()
        {
            return View();
        }
        public IActionResult Fixtures()
        {
            return View();
        }


        private int CurrentManager()
        {
            return 1;
        }

    }
}
