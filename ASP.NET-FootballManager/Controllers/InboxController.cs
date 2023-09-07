namespace ASP.NET_FootballManager.Controllers
{
    using FootballManager.Core.Services;
    using FootballManager.Infrastructure.Data.DataModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    public class InboxController : Controller
    {
        private readonly ServiceAggregator serviceAggregator;
        public InboxController(ServiceAggregator serviceAggregator)
        {
            this.serviceAggregator = serviceAggregator;
        }
        public async Task<IActionResult> Index(int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.gameService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var currentMessage = await serviceAggregator.inboxService.GetFullMessage(id, CurrentGame);
            var inboxViewModel = serviceAggregator.modelService.GetInboxViewModel(currentMessage, CurrentGame.Id);

            return View(inboxViewModel);
        }
        public async Task<IActionResult> OpenNews(int newsId)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.gameService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var currentMessage = await serviceAggregator.inboxService.GetFullMessage(newsId, CurrentGame);
            var inboxViewModel = serviceAggregator.modelService.GetInboxViewModel(currentMessage, CurrentGame.Id);

            return View("Index", inboxViewModel);
        }
    }
}
