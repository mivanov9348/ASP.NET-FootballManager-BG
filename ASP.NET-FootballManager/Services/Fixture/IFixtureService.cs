namespace ASP.NET_FootballManager.Services.Fixture
{
    using ASP.NET_FootballManager.Data.DataModels;
    public interface IFixtureService
    {
        void GenerateLeagueFixtures(Game game);
        void GenerateCupFixtures(Game game);
        void GenerateEuroFixtures(Game game);
        void AddFixtureToDay(Game game);
        void ShuffleTeams(List<VirtualTeam> currl);
        void DeleteFixtures(Game game);
        List<Fixture> GetFixture(int id, int round);    
        int GetAllRounds(int leagueId);


    }
}
