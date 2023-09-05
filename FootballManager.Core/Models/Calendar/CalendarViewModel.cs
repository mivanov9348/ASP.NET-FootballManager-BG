namespace FootballManager.Core.Models.Calendar
{
    using FootballManager.Infrastructure.Data.DataModels.Calendar;

    public class CalendarViewModel
    {
        public string MonthName { get; set; }
        public int MonthId { get; set; }
        public int StartOffsetDays { get; set; }
        public int EndOffsetDays { get; set; }
        public int CurrentDayOrder { get; set; }
        public List<Day> Days { get; set; } = new List<Day>();
        public int Year { get; set; }




    }
}
