namespace ASP.NET_FootballManager.Controllers
{
    using FootballManager.Core.Services;
    using FootballManager.Infrastructure.Data.DataModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    public class InboxController : Controller
    {
        private readonly ServiceAggregator _serviceAggregator;

        public InboxController(ServiceAggregator serviceAggregator)
        {
            _serviceAggregator = serviceAggregator ?? throw new ArgumentNullException(nameof(serviceAggregator));
        }

        public async Task<IActionResult> Index(int id)
        {
            var gameInfo = GetCurrentGameInfo();
            if (gameInfo is null)
                return Unauthorized();

            var (_, _, currentGame, _) = gameInfo.Value; // Разпакетиране след проверка за null
            var inboxViewModel = await GetInboxViewModel(id, currentGame);
            return View(inboxViewModel);
        }

        public async Task<IActionResult> OpenNews(int newsId)
        {
            var gameInfo = GetCurrentGameInfo();
            if (gameInfo is null)
                return Unauthorized();

            var (_, _, currentGame, _) = gameInfo.Value; // Разпакетиране след проверка за null
            var inboxViewModel = await GetInboxViewModel(newsId, currentGame);
            return View("Index", inboxViewModel);
        }

        private (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam)? GetCurrentGameInfo()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return null;

            return _serviceAggregator.gameService.CurrentGameInfo(userId);
        }

        private async Task<object> GetInboxViewModel(int messageId, Game currentGame)
        {
            var currentMessage = await _serviceAggregator.inboxService.GetFullMessage(messageId, currentGame);
            return _serviceAggregator.modelService.GetInboxViewModel(currentMessage, currentGame.Id);
        }
    }
}