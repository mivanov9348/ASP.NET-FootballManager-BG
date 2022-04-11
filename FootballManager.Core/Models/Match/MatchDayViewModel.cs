namespace FootballManager.Core.Models.Match
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    public class MatchDayViewModel
    {
        public VirtualTeam CurrentTeam { get; set; }
        public List<Fixture> DayFixtures {get;set;}= new List<Fixture>();
        public List<League> Leagues { get; set; } = new List<League>();
        public int Year { get; set; }
        public int Day { get; set; }
        public int Round { get; set; }
        public string CompetitionName { get; set; }
    }
}
