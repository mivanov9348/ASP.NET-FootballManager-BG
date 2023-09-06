namespace FootballManager.Core.Models.League
{
    using FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Menu;

    public class FixturesViewModel
    {
        public int LeagueId { get; set; }
        public string OptionSelect { get; set; }
        public int CurrentRound { get; set; }
        public int AllRounds { get; set; }
        public string CurrentLeagueName { get; set; }
        public List<League> Leagues { get; set; } = new List<League>();
        public List<Fixture> Fixtures { get; set; } = new List<Fixture>();
        public MenuViewModel MenuViewModel { get; set; }






    }
}
