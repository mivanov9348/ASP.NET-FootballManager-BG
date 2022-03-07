namespace ASP.NET_FootballManager.Models
{
    using ASP.NET_FootballManager.Data.DataModels;
    public class MatchDayViewModel
    {

        public List<Fixture> DayFixtures {get;set;}= new List<Fixture>();
        public List<League> Leagues { get; set; } = new List<League>();
        public int Year { get; set; }
        public int Day { get; set; }
        public int Round { get; set; }
    }
}
