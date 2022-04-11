namespace FootballManager.Core.Models.Menu
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    public class CalendarViewModel
    {

        public List<Day> Days { get; set; } = new List<Day>();

        public int Year { get; set; }




    }
}
