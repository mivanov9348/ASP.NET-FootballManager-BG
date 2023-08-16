namespace ASP.NET_FootballManager.Services.Inbox
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.Constant;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Menu;
    using FootballManager.Infrastructure.Data.Constant;
    using System;
    using System.Numerics;
    using System.Text;
    using static ASP.NET_FootballManager.Data.Constant.DataConstants;

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

            var newSeasonNews = new Inbox
            {
                Day = currentGame.Day,
                Year = currentGame.Year,
                Game = currentGame,
                GameId = currentGame.Id,
                MessageReview = messageReview,
                FullMessage = fullMessage,
                NewsImage = $"Faces/Manager.png"
            };

            AddAndSave(newSeasonNews);
        }
        public void BuyPlayerNews(Player currentPlayer, Game currentGame)
        {
            var team = this.data.VirtualTeams.FirstOrDefault(x => x.TeamId == currentGame.TeamId);
            var position = this.data.Positions.FirstOrDefault(x => x.Id == currentPlayer.PositionId);

            var randomMessageFunc = inboxMessages.NewPlayer[rnd.Next(0, inboxMessages.NewPlayer.Count())];
            string randomMessage = randomMessageFunc(team.Name, currentPlayer.FirstName, currentPlayer.LastName, currentPlayer.Age, currentPlayer.Position.Name, currentPlayer.Price);

            var inbox = new Inbox
            {
                Day = currentGame.Day,
                Year = currentGame.Year,
                Game = currentGame,
                GameId = currentGame.Id,
                MessageReview = randomMessage,
                FullMessage = randomMessage,
                NewsImage = $"Faces/{currentPlayer.ProfileImage}"
            };
            AddAndSave(inbox);
        }
        public void NewSeasonNews(Game currentGame)
        {
            var randomMessageFunc = inboxMessages.NewSeasonStart[rnd.Next(0, inboxMessages.NewSeasonStart.Count())];
            string randomMessage = randomMessageFunc(currentGame.Team.Name, currentGame.Season);

            var newSeasonNews = new Inbox
            {
                Day = currentGame.Day,
                Year = currentGame.Year,
                Game = currentGame,
                GameId = currentGame.Id,
                MessageReview = randomMessage,
                FullMessage = randomMessage
            };

            AddAndSave(newSeasonNews);
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

            var matchNews = new Inbox
            {
                Day = CurrentGame.Day,
                Year = CurrentGame.Year,
                Game = CurrentGame,
                GameId = CurrentGame.Id,
                MessageReview = fullMessage,
                FullMessage = fullMessage,
                NewsImage = $"Team/{playerTeam.ImageUrl}"
            };

            AddAndSave(matchNews);
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

            var inbox = new Inbox
            {
                Day = currentGame.Day,
                Year = currentGame.Year,
                Game = currentGame,
                GameId = currentGame.Id,
                MessageReview = randomMessage,
                FullMessage = randomMessage,
                NewsImage = $"Faces/{player.ProfileImage}"
            };
            AddAndSave(inbox);
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

            var MatchesInfo = new Inbox
            {
                Day = CurrentGame.Day,
                Year = CurrentGame.Year,
                Game = CurrentGame,
                GameId = CurrentGame.Id,
                MessageReview = messageReview,
                FullMessage = fullMessage
            };

            AddAndSave(MatchesInfo);

        }
        private void AddAndSave(Inbox inbox)
        {
            this.data.Inboxes.Add(inbox);
            this.data.SaveChanges();
        }

        public async Task<InboxViewModel> GetInboxViewModel(Inbox currentMessage, int gameId)
        {
            var currentInboxMessages = await GetInboxMessages(gameId);
            var newInboxViewModel = new InboxViewModel
            {
                News =  currentInboxMessages.OrderByDescending(x => x.Id).ToList(),
                CurrentNews = currentMessage,
                Year = currentMessage.Year,
                Day = currentMessage.Day

            };
            return newInboxViewModel;
        }
    }
}
