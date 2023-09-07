namespace ASP.NET_FootballManager.Controllers
{
    using FootballManager.Core.Models.Calendar;
    using FootballManager.Core.Services;
    using Microsoft.AspNetCore.Mvc;   
    using System.Security.Claims; 
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
            var newCalendarModel = serviceAggregator.modelService.GetCalendarViewModel(currentDate.month);

            return View("Index", newCalendarModel);
        }

        public async Task<IActionResult> PreviousMonth(CalendarViewModel model, int monthId)
        {
            var month = serviceAggregator.calendarService.ReturnPreviousMonth(monthId);
            var newCalendarModel = serviceAggregator.modelService.GetCalendarViewModel(month);         

            return View("Index", newCalendarModel);
        }

        public async Task<IActionResult> NextMonth(CalendarViewModel model, int monthId)
        {
            var month = serviceAggregator.calendarService.NextMonth(monthId);
            var newCalendarModel = serviceAggregator.modelService.GetCalendarViewModel(month);
           
            return View("Index", newCalendarModel);
        }

        public async Task<IActionResult> NextDay()
        {
            CurrentUser();
            var currentGame = serviceAggregator.gameService.GetCurrentGame(userId);          
            serviceAggregator.calendarService.NextDay(currentGame);
            var currentDate = serviceAggregator.calendarService.GetCurrentDate(currentGame);
            var newCalendarModel = serviceAggregator.modelService.GetCalendarViewModel(currentDate.month);
        
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
