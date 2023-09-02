namespace FootballManager.Infrastructure.Data.DataModels.Calendar
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using System.ComponentModel.DataAnnotations;
    public class Day
    {
        public int Id { get; set; }
        public string DayName { get; set; }
        public int DayOrder { get; set; }
        public int WeekDayOrder { get; set; }
        public bool IsMatchDay { get; set; }
        public bool IsEuroCupDay { get; set; }
        public bool IsCupDay { get; set; }
        public bool IsLeagueDay { get; set; }
        public bool IsDrawDay { get; set; }
        public bool IsPlayed { get; set; }
        public int YearId { get; set; }
        public Year Year { get; set; }
        public int MonthId { get; set; }
        public Month Month { get; set; }
        public int GameId { get; set; }
        public Week Week { get; set; }
        public int WeekId { get; set; }
        public Game Game { get; set; }
        public ICollection<Fixture> Fixtures { get; set; } = new HashSet<Fixture>();

    }
}

