namespace FootballManager.Infrastructure.Data.DataModels.Calendar
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using System.ComponentModel.DataAnnotations;
    public class Day
    {
        public int Id { get; set; }
        public int CurrentDay { get; set; }
        public bool isMatchDay { get; set; }
        public bool isEuroCupDay { get; set; }
        public bool isCupDay { get; set; }
        public bool isLeagueDay { get; set; }
        public bool IsDrawDay { get; set; }
        public bool IsPlayed { get; set; }
        public int YearId { get; set; }
        public Year Year { get; set; }
        public int MonthId { get; set; }
        public Month Month { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public ICollection<Fixture> Fixtures { get; set; } = new HashSet<Fixture>();

    }
}

