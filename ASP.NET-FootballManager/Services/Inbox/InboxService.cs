namespace ASP.NET_FootballManager.Services.Inbox
{
    using ASP.NET_FootballManager.Data;
    using NET_FootballManager.Data.DataModels;
    public class InboxService : IInboxService
    {
        private readonly FootballManagerDbContext data;
        public InboxService(FootballManagerDbContext data)
        {
            this.data = data;
        }

        public void CreateManagerNews(Manager currentManager, Game currentGame)
        {
            var inbox = new Inbox
            {
                Day = currentGame.Day,
                Year = currentGame.Year,
                Game = currentGame,
                GameId = currentGame.Id,
                Message = $"{currentGame.Team.Name} appoint {currentManager.FirstName} {currentManager.LastName} as manager!"
            };
            AddAndSave(inbox);
        }

        public void BuyPlayerNews(Player currentPlayer, Game currentGame)
        {
            var inbox = new Inbox
            {
                Day = currentGame.Day,
                Year = currentGame.Year,
                Game = currentGame,
                GameId = currentGame.Id,
                Message = $"{currentPlayer.FirstName} {currentPlayer.LastName} is a new {currentGame.Team.Name} player!"
            };
            AddAndSave(inbox);
        }

        public void MatchFinishedNews(string winnerTeam, string LosingTeam, Game currentGame)
        {

            var inbox = new Inbox
            {
                Day = currentGame.Day,
                Year = currentGame.Year,
                Game = currentGame,
                GameId = currentGame.Id
            };

            AddAndSave(inbox);
        }

        private void AddAndSave(Inbox inbox)
        {
            this.data.Inboxes.Add(inbox);
            this.data.SaveChanges();
        }
    }
}
