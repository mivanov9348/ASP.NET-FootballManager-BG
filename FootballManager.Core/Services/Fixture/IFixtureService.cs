namespace ASP.NET_FootballManager.Services.Fixture
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    public interface IFixtureService
    {
        void GenerateLeagueFixtures(Game game);        
        void AddFixtureToDay(Game game);
        void ShuffleTeams(List<VirtualTeam> currl);
        void DeleteFixtures(Game game);
        Task<List<Fixture>> GetFixture(int id, int round,Game CurrentGame);    
        Task<int> GetAllRounds(int? leagueId);


    }
}
