namespace ASP.NET_FootballManager.Services.Common
{
    using ASP.NET_FootballManager.Data.DataModels;
    public interface IDayService
    {     
        void CalculateDays(Game currentGame);
        Day GetCurrentDay(Game currentGame);
        List<Day> GetAllDays(Game currentGame);
    }

}