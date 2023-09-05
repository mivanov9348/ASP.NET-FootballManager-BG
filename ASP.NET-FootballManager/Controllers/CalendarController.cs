using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
using FootballManager.Core.Models.Calendar;
using FootballManager.Core.Services;
using FootballManager.Infrastructure.Data.DataModels.Calendar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
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
        public async Task<IActionResult> Index(CalendarViewModel model)
        {
            CurrentUser();
            var currentGame = this.serviceAggregator.gameService.GetCurrentGame(userId);
            var currentDate = serviceAggregator.calendarService.GetCurrentDate(currentGame);          
            var newCalendarModel = serviceAggregator.calendarService.GetCalendarViewModel(currentDate.month);

            return View("Index", newCalendarModel);
        }

        public async Task<IActionResult> PreviousMonth(CalendarViewModel model, int monthId)
        {
            var month = serviceAggregator.calendarService.ReturnPreviousMonth(monthId);
            var newCalendarModel = serviceAggregator.calendarService.GetCalendarViewModel(month);

            return View("Index", newCalendarModel);
        }

        public async Task<IActionResult> NextMonth(CalendarViewModel model, int monthId)
        {
            var month = serviceAggregator.calendarService.NextMonth(monthId);
            var newCalendarModel = serviceAggregator.calendarService.GetCalendarViewModel(month);
           
            return View("Index", newCalendarModel);
        }

        public async Task<IActionResult> NextDay()
        {
            CurrentUser();
            var currentGame = serviceAggregator.gameService.GetCurrentGame(userId);          
            serviceAggregator.calendarService.NextDay(currentGame);
            var currentDate = serviceAggregator.calendarService.GetCurrentDate(currentGame);
            var newCalendarModel = serviceAggregator.calendarService.GetCalendarViewModel(currentDate.month);
        
            return View("Index", newCalendarModel);
           
        }

        private void CurrentUser()
        {
            if (this.User.Identity.IsAuthenticated != false)
            {
                userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
        }

    }
}
