namespace ASP.NET_FootballManager.Services.Cup
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    public interface ICupService
    {

        void GenerateCupParticipants(Game curentGame);
        void CheckWinner(Fixture currentFixture);
        void CalculateOtherMatches(List<Fixture> dayFixtures, Fixture currentFixture);
        Task<List<Fixture>> GetCupFixtures(Game CurrentGame);
        Task<Cup> GetCurrentCup();
        Task<VirtualTeam> GetWinner(Game game);

    }
}
