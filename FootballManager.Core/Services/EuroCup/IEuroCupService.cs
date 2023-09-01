namespace ASP.NET_FootballManager.Services.EuroCup
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Infrastructure.Data.DataModels.Calendar;

    public interface IEuroCupService
    {
        void CreateChampionsCup(Game game, Year year);
        void CreateEuroCup(Game game, Year year);
        void FillChampionsLeagueParticipants(Game game);
        void FillEuroCupParticipants(Game game);
        void CheckWinner(Fixture currentFixture);
        void CalculateOtherMatches(List<Fixture> dayFixtures, Fixture currentFixture);
        Task<EuropeanCup> GetEuropeanCup(int cupId);
        Task<List<EuropeanCup>> AllEuroCups();
        Task<VirtualTeam> GetChampionsCupWinner(Game game);
        Task<VirtualTeam> GetEuroCupWinner(Game game);
        Task<List<Fixture>> GetEuroCupFixtures(Game CurrentGame, int euroCupRank);
    }
}
