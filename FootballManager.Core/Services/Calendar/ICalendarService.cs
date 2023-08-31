namespace FootballManager.Core.Services.Calendar
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Infrastructure.Data.DataModels.Calendar;
    public interface ICalendarService
    {
        Year GenerateYear(Game currentGame);
        void GenerateMonths(Game currentGame, Year currentYear);
        void GenerateWeeks(Month newMonth, Month previousMonth, Year newYear, Game currentGame, int weeksCount);
        void GenerateDays(Game currentGame, int dayOrder, int dayOfWeekIndex, Year currentYear, Month currentMonth, Week currentWeek);



        Year GetCurrentYear();
        Month GetCurrentMonth();
        Task<Day> GetCurrentDay(Game currentGame);
        Task<List<Day>> GetAllDays(Game currentGame);
    }
}
