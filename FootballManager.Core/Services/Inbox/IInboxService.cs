namespace ASP.NET_FootballManager.Services.Inbox
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Menu;

    public interface IInboxService
    {
        void CreateManagerNews(Manager currentManager, Game currentGame);
        void BuyPlayerNews(Player currentPlayer, Game currentGame);
        void SellPlayerNews(int playerId, Game currentGame);
        void NewSeasonNews(Game currentGame);
        void MatchFinishedNews(Game CurrentGame, Fixture currentFixture);
        Task<List<Inbox>> GetInboxMessages(int gameId);
        Task<Inbox> GetFullMessage(int id, Game CurrentGame);
        void CupMatchesInfo(List<Fixture> dayFixtures,Game CurrentGame);
        Task<InboxViewModel> GetInboxViewModel(Inbox currentMessage, int gameId);
        void CreateInbox(Game currentGame, string fullMessage, string messageTitle, string root);
    }
}
