namespace FootballManager.Core.Models.Menu
{
    using FootballManager.Infrastructure.Data.DataModels.Calendar;

    public class CalendarViewModel
    {

        public List<Day> Days { get; set; } = new List<Day>();

        public int Year { get; set; }




    }
}
