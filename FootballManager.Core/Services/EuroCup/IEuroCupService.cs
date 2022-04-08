namespace ASP.NET_FootballManager.Services.EuroCup
{
    using Data.DataModels;
    public interface IEuroCupService
    { 
        void DistributionEuroParticipant(Game game);       
        void CheckWinner(Fixture currentFixture);
        void CalculateOtherMatches(List<Fixture> dayFixtures, Fixture currentFixture);
        EuropeanCup GetEuropeanCup(int cupId);
        List<EuropeanCup> AllEuroCups();
        VirtualTeam GetChampionsCupWinner(Game game);
        VirtualTeam GetEuroCupWinner(Game game);
        List<Fixture> GetEuroCupFixtures(Game CurrentGame, int euroCupRank);
    }
}
