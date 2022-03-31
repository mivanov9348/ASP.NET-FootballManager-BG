namespace ASP.NET_FootballManager.Services.Cup
{
    using Data.DataModels;
    public interface ICupService
    {

        void GenerateCupParticipants(Game curentGame);
        void CheckWinner(Fixture currentFixture);
        void CalculateOtherMatches(List<Fixture> dayFixtures, Fixture currentFixture);
        Cup GetCurrentCup();
        VirtualTeam GetWinner(Game game);

    }
}
