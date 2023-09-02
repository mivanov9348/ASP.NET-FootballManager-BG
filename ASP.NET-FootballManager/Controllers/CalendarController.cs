using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
using FootballManager.Core.Models.Menu;
using FootballManager.Core.Services;
using FootballManager.Infrastructure.Data.DataModels.Calendar;
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
        public async Task<IActionResult> Index(CalendarViewModel model)
        {
            CurrentUser();
            var currentGame = this.serviceAggregator.gameService.GetCurrentGame(userId);
            var currentYear = serviceAggregator.calendarService.GetCurrentYear(currentGame);
            var currentMonth = serviceAggregator.calendarService.GetCurrentMonth(currentYear, model.MonthId);
            var monthDays = await serviceAggregator.calendarService.GetAllDaysInMonth(currentMonth);

            return View(new CalendarViewModel
            {
                MonthName = currentMonth.MonthName,
                Days = monthDays,
                Year = currentYear.YearOrder,
                MonthId = currentMonth.Id
            });
        }

        public async Task<IActionResult> PreviousMonth(CalendarViewModel model, int monthId)
        {
            var month = serviceAggregator.calendarService.ReturnPreviousMonth(monthId);
            var monthDays = await serviceAggregator.calendarService.GetAllDaysInMonth(month);

            return View("Index", new CalendarViewModel
            {
                MonthName = month.MonthName,
                Days = monthDays,
                Year = month.Year.YearOrder,
                MonthId = month.Id
            });
        }

        public async Task<IActionResult> NextMonth(CalendarViewModel model, int monthId)
        {
            var month = serviceAggregator.calendarService.NextMonth(monthId);
            var monthDays = await serviceAggregator.calendarService.GetAllDaysInMonth(month);

            return View("Index", new CalendarViewModel
            {
                MonthName = month.MonthName,
                Days = monthDays,
                Year = month.Year.YearOrder,
                MonthId = month.Id
            });
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
