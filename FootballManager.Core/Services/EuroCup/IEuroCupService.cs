namespace ASP.NET_FootballManager.Services.EuroCup
{
    using FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Infrastructure.Data.DataModels.Calendar;

    public interface IEuroCupService
    {
        void RemoveEuropeanParticipants(Game game);
        void CreateChampionsCup(Game game, Year year);
        void CreateEuroCup(Game game, Year year);
        void FillChampionsCupParticipants(Game game);
        void FillEuroCupParticipants(Game game);
        void CheckWinner(Fixture currentFixture);
        void CalculateOtherMatches(List<Fixture> dayFixtures, Fixture currentFixture);
        Task<ContinentalCup> GetEuropeanCup(int cupId);
        Task<List<ContinentalCup>> AllEuroCups();
        Task<VirtualTeam> GetChampionsCupWinner(Game game);
        Task<VirtualTeam> GetEuroCupWinner(Game game);
        Task<List<Fixture>> GetEuroCupFixtures(Game CurrentGame, int euroCupRank);
        List<ContinentalCup> GetYearEuropeanCups(Game currentGame);
        Object GetEuropeanCupByObject(object cup);
    }
}
