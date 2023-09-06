namespace FootballManager.Core.Services.Calendar
{
    using FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Calendar;
    using FootballManager.Infrastructure.Data.DataModels.Calendar;
    public interface ICalendarService
    {
        Year GenerateYear(Game currentGame);
        void GenerateMonths(Game currentGame, Year currentYear);
        void GenerateWeeks(Month newMonth, Month previousMonth, Year newYear, Game currentGame, int weeksCount);
        void GenerateDays(Game currentGame, int dayOrder, int dayOfWeekIndex, Year currentYear, Month currentMonth, Week currentWeek);
        int GetStartOffsetDays(Month currentMonth);
        int GetEndOffsetDays(Month currentMonth);
        void NextDay(Game currentGame);
        (Year year, Month month, Day day) GetCurrentDate(Game currentGame);
        Month ReturnPreviousMonth(int monthId);
        Month NextMonth(int monthId);
        Task SetWeekPlan(Game currentGame, Year currentYear);
        List<Day> GetAllDaysInMonth(Month month);


    }
}
