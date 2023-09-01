﻿using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
using FootballManager.Core.Models.Menu;
using FootballManager.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace ASP.NET_FootballManager.Controllers
{
    public class CalendarController : Controller
    {
        private readonly ServiceAggregator serviceAggregator;
        private string userId;
        public CalendarController(ServiceAggregator serviceAggregator)
        {
            this.serviceAggregator = serviceAggregator;
        }
        public async Task<IActionResult> Index()
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var days = await serviceAggregator.calendarService.GetAllDays(CurrentGame);
            var year = CurrentGame.Year;

            return View(new CalendarViewModel
            {
                Days = days,
                Year = year
            });
        }              

        
    }
}
