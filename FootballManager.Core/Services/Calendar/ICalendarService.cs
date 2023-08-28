namespace FootballManager.Core.Services.Calendar
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Infrastructure.Data.DataModels.Calendar;
    public interface ICalendarService
    {
        Year CreateYear(Game currentGame);
        Year GetCurrentYear();
        Month GetCurrentMonth();
        Day GetCurrentDay(Game currentGame);    
        
    }
}
