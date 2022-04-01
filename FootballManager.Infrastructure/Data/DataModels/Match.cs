namespace ASP.NET_FootballManager.Data.DataModels
{
    public class Match
    {
        public int Id { get; set; }
        public int Minute { get; set; }
        public string SituationText { get; set; }
        public int Turn { get; set; }
        public bool isEnd { get; set; }
        public Fixture CurrentFixture { get; set; }
        public int CurrentFixtureId { get; set; }
        public Game Game { get; set; }
        public int GameId { get; set; }



    }
}
