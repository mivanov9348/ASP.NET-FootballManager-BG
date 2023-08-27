namespace ASP.NET_FootballManager.Services.Common
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Infrastructure.Data.DataModels.Calendar;

    public interface IDayService
    {     
        void CalculateDays(Game currentGame);
        Task<Day> GetCurrentDay(Game currentGame);
        Task<List<Day>> GetAllDays(Game currentGame);
    }

}