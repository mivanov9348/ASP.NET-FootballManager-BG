namespace ASP.NET_FootballManager.Services.Inbox
{
    using NET_FootballManager.Data.DataModels;
    public interface IInboxService
    {

        void CreateManagerNews(Manager currentManager, Game currentGame);
        void TransferPlayerNews(Player currentPlayer, Game currentGame);
        void MatchFinishedNews(string winnerTeam, string LosingTeam, Game currentGame);

    }
}
