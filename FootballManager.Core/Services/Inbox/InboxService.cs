namespace ASP.NET_FootballManager.Services.Inbox
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Menu;
    using System;
    using System.Numerics;
    using System.Text;
    public class InboxService : IInboxService
    {
        private Random rnd;
        private readonly FootballManagerDbContext data;
        private readonly FootballManager.Infrastructure.Data.Constant.InboxMessages.Messages inboxMessages;
        public InboxService(FootballManagerDbContext data)
        {
            this.data = data;
            this.inboxMessages = new FootballManager.Infrastructure.Data.Constant.InboxMessages.Messages();
            this.rnd = new Random();
        }
        public void CreateManagerNews(Manager currentManager, Game currentGame)
        {
            var messageReview = $"{currentGame.Team.Name} appoint {currentGame.Manager.FirstName} {currentGame.Manager.LastName} as manager!";
            var fullMessage = $"Welcome to the new club! Season {currentGame.Season} started! Good luck!";
            var messageTitle = "New Manager";
            var root = $"Managers/{currentManager.ImageId}.png";
            CreateInbox(currentGame, fullMessage, messageTitle, null);

        }
        public void BuyPlayerNews(Player currentPlayer, Game currentGame)
        {
            var team = this.data.VirtualTeams.FirstOrDefault(x => x.TeamId == currentGame.TeamId);
            var position = this.data.Positions.FirstOrDefault(x => x.Id == currentPlayer.PositionId);

            var randomMessageFunc = inboxMessages.NewPlayer[rnd.Next(0, inboxMessages.NewPlayer.Count())];
            string randomMessage = randomMessageFunc(team.Name, currentPlayer.FirstName, currentPlayer.LastName, currentPlayer.Age, currentPlayer.Position.Name, currentPlayer.Price);
            string messageTitle = "New Player";
            string root = $"Faces/{currentPlayer.ProfileImage}";
            CreateInbox(currentGame, randomMessage, messageTitle, root);

        }
        public void NewSeasonNews(Game currentGame)
        {
            var randomMessageFunc = inboxMessages.NewSeasonStart[rnd.Next(0, inboxMessages.NewSeasonStart.Count())];
            string randomMessage = randomMessageFunc(currentGame.Team.Name, currentGame.Season);
            string messageTitle = "Welcome to the New Season";

            CreateInbox(currentGame, randomMessage, messageTitle, null);
        }
        public void MatchFinishedNews(Game CurrentGame, Fixture currentFixture)
        {
            var messageReview = "";
            var fullMessage = "";
            var homeTeamName = this.data.VirtualTeams.FirstOrDefault(x => x.Id == currentFixture.HomeTeamId).Name;
            var awayTeamName = this.data.VirtualTeams.FirstOrDefault(x => x.Id == currentFixture.AwayTeamId).Name;
            var playerTeam = this.data.VirtualTeams.FirstOrDefault(x => x.Id == currentFixture.AwayTeamId || x.Id == currentFixture.HomeTeamId);

            if (currentFixture.HomeTeamGoal > currentFixture.AwayTeamGoal)
            {
                var randomMessageFunc = inboxMessages.FinishMatch[rnd.Next(0, inboxMessages.FinishMatch.Count())];
                fullMessage = randomMessageFunc(homeTeamName, awayTeamName, currentFixture.HomeTeamGoal, currentFixture.AwayTeamGoal, currentFixture.Round);
            }
            if (currentFixture.HomeTeamGoal < currentFixture.AwayTeamGoal)
            {
                var randomMessageFunc = inboxMessages.FinishMatch[rnd.Next(0, inboxMessages.FinishMatch.Count())];
                fullMessage = randomMessageFunc(awayTeamName, homeTeamName, currentFixture.AwayTeamGoal, currentFixture.HomeTeamGoal, currentFixture.Round);
            }
            if (currentFixture.HomeTeamGoal == currentFixture.AwayTeamGoal)
            {
                var randomMessageFunc = inboxMessages.DrawMatch[rnd.Next(0, inboxMessages.DrawMatch.Count())];
                fullMessage = randomMessageFunc(homeTeamName, awayTeamName, currentFixture.HomeTeamGoal, currentFixture.AwayTeamGoal, currentFixture.Round);
            }

            var messageTitle = $"{homeTeamName} - {awayTeamName} {currentFixture.HomeTeamGoal}:{currentFixture.AwayTeamGoal}";

            CreateInbox(CurrentGame, fullMessage, messageTitle, null);

        }
        public async Task<List<Inbox>> GetInboxMessages(int gameId) => await Task.Run(() => this.data.Inboxes.Where(x => x.GameId == gameId).ToList());
        public async Task<Inbox> GetFullMessage(int id, Game CurrentGame)
        {
            if (id == 0)
            {
                return await Task.Run(() => this.data.Inboxes.OrderByDescending(x => x.Id).FirstOrDefault(x => x.GameId == CurrentGame.Id));
            }
            else
            {
                return await Task.Run(() => this.data.Inboxes.FirstOrDefault(x => x.Id == id));
            }

        }
        public void SellPlayerNews(int playerId, Game currentGame)
        {
            var team = this.data.VirtualTeams.FirstOrDefault(x => x.TeamId == currentGame.TeamId);
            var player = this.data.Players.FirstOrDefault(x => x.Id == playerId);
            var position = this.data.Positions.FirstOrDefault(x => x.Id == player.PositionId);

            var randomMessageFunc = inboxMessages.SellPlayer[rnd.Next(0, inboxMessages.SellPlayer.Count())];
            string randomMessage = randomMessageFunc(team.Name, player.FirstName, player.LastName, player.Age, player.Position.Name, player.Price);
            string messageTitle = "Sell Player";
            string root = $"Faces/{player.ProfileImage}";
            CreateInbox(currentGame, randomMessage, messageTitle, root);
        }
        public void CupMatchesInfo(List<Fixture> dayFixtures, Game CurrentGame)
        {
            var sb = new StringBuilder();

            foreach (var fixture in dayFixtures)
            {
                sb.AppendLine($"{fixture.HomeTeamName} {fixture.HomeTeamGoal}:{fixture.AwayTeamGoal} {fixture.AwayTeamName}");
                sb.AppendLine(Environment.NewLine);
            }

            var messageReview = "Results..";
            var fullMessage = sb.ToString().TrimEnd();
            var messageTitle = "Cup Results";

            CreateInbox(CurrentGame, fullMessage, messageTitle, null);

        }

        public async Task<InboxViewModel> GetInboxViewModel(Inbox currentMessage, int gameId)
        {
            var currentInboxMessages = await GetInboxMessages(gameId);
            var newInboxViewModel = new InboxViewModel
            {
                News = currentInboxMessages.OrderByDescending(x => x.Id).ToList(),
                CurrentNews = currentMessage,
                Year = currentMessage.Year,
                Day = currentMessage.Day

            };
            return newInboxViewModel;
        }

        public void CreateInbox(Game currentGame, string fullMessage, string messageTitle, string imageRoot)
        {
            var newInbox = new Inbox
            {
                Day = currentGame.Day,
                Year = currentGame.Year,
                Game = currentGame,
                GameId = currentGame.Id,
                MessageTitle = messageTitle,
                FullMessage = fullMessage,
                NewsImage = imageRoot
            };

            this.data.Inboxes.Add(newInbox);
            this.data.SaveChanges();
        }
    }
}
