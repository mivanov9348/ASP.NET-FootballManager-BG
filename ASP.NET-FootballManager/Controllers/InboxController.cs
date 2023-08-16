﻿namespace ASP.NET_FootballManager.Controllers
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Menu;
    using FootballManager.Core.Services;
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
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var currentInboxMessages = await serviceAggregator.inboxService.GetInboxMessages(CurrentGame.Id);
            var currentMessage = await serviceAggregator.inboxService.GetFullMessage(id, CurrentGame);

            return View(new InboxViewModel
            {
                News = currentInboxMessages.OrderByDescending(x => x.Id).ToList(),
                CurrentNews = currentMessage,
                Year = currentMessage.Year,
                Day = currentMessage.Day
            });
        }
        public async Task<IActionResult> OpenNews(int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var currentInboxMessage = await serviceAggregator.inboxService.GetInboxMessages(CurrentGame.Id);
            var currentMessage = await serviceAggregator.inboxService.GetFullMessage(id, CurrentGame);

            return View("Index", new InboxViewModel
            {
                News = currentInboxMessage.OrderByDescending(x => x.Id).ToList(),
                CurrentNews = currentMessage
            });
        }
    }
}
