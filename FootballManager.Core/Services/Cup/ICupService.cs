namespace ASP.NET_FootballManager.Services.Cup
{
    using FootballManager.Infrastructure.Data.DataModels;
    public interface ICupService
    {
        void GenerateCupParticipants(Game curentGame);
        void CheckWinner(Fixture currentFixture);
        void CalculateOtherMatches(List<Fixture> dayFixtures, Fixture currentFixture);
        Task<List<Fixture>> GetCupFixtures(Game CurrentGame);
        Cup GetCurrentCup(Game currentGame);
        Task<VirtualTeam> GetWinner(Game game);
        void CreateCups(Game currentGame);
    }
}
