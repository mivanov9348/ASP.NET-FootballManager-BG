namespace ASP.NET_FootballManager.Models
{
    using ASP.NET_FootballManager.Data.DataModels;
    public class FixturesViewModel
    {
        public int LeagueId { get; set; }
        public int CurrentRound { get; set; }
        public int AllRounds { get; set; }
        public List<League> Leagues { get; set; } = new List<League>();
        public List<Fixture> Fixtures { get; set; } = new List<Fixture>();







    }
}
