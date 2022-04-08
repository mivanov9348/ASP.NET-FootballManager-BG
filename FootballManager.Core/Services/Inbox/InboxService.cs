namespace ASP.NET_FootballManager.Services.Inbox
{
    using ASP.NET_FootballManager.Data;
    using NET_FootballManager.Data.DataModels;
    using System.Text;

    public class InboxService : IInboxService
    {
        private readonly FootballManagerDbContext data;

        public InboxService(FootballManagerDbContext data)
        {
            this.data = data;
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

            var messageReview = $"{team.Name} confirm transfer!";
            var fullMessage = $"{currentPlayer.FirstName} {currentPlayer.LastName} is a new {team.Name} player. {currentPlayer.FirstName} {currentPlayer.LastName} is a  {currentPlayer.Age} years old, play as {position.Name}.";

            var inbox = new Inbox
            {
                Day = currentGame.Day,
                Year = currentGame.Year,
                Game = currentGame,
                GameId = currentGame.Id,
                MessageReview = messageReview,
                FullMessage = fullMessage,
                NewsImage = $"Faces/{currentPlayer.ProfileImage}"
            };
            AddAndSave(inbox);
        }
        public void NewSeasonNews(Game currentGame)
        {
            var messageReview = $"Season {currentGame.Season} started!";
            var fullMessage = $"Season {currentGame.Season} started! Good luck!";

            var newSeasonNews = new Inbox
            {
                Day = currentGame.Day,
                Year = currentGame.Year,
                Game = currentGame,
                GameId = currentGame.Id,
                MessageReview = messageReview,
                FullMessage = fullMessage
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
                messageReview = $"{homeTeamName} - {awayTeamName} {currentFixture.HomeTeamGoal}:{currentFixture.AwayTeamGoal} ";
                fullMessage = $"{homeTeamName} wins over {awayTeamName} with {currentFixture.HomeTeamGoal}:{currentFixture.AwayTeamGoal} in round {currentFixture.Round}.";
            }
            if (currentFixture.HomeTeamGoal < currentFixture.AwayTeamGoal)
            {
                messageReview = $"{homeTeamName} - {awayTeamName} {currentFixture.HomeTeamGoal}:{currentFixture.AwayTeamGoal} ";
                fullMessage = $"{awayTeamName} wins over {homeTeamName} with {currentFixture.AwayTeamGoal}:{currentFixture.HomeTeamGoal} in round {currentFixture.Round}.";
            }
            if (currentFixture.HomeTeamGoal == currentFixture.AwayTeamGoal)
            {
                messageReview = $"{homeTeamName} - {awayTeamName} {currentFixture.HomeTeamGoal}:{currentFixture.AwayTeamGoal}";
                fullMessage = $"{homeTeamName} finished draw with {awayTeamName} in round {currentFixture.Round}.";
            }

            var matchNews = new Inbox
            {
                Day = CurrentGame.Day,
                Year = CurrentGame.Year,
                Game = CurrentGame,
                GameId = CurrentGame.Id,
                MessageReview = messageReview,
                FullMessage = fullMessage,
                NewsImage = $"Team/{playerTeam.ImageUrl}"
            };

            AddAndSave(matchNews);
        }
        public List<Inbox> GetInboxMessages(int gameId) => this.data.Inboxes.Where(x => x.GameId == gameId).ToList();
        public Inbox GetFullMessage(int id, Game CurrentGame)
        {
            if (id == 0)
            {
                return this.data.Inboxes.OrderByDescending(x => x.Id).FirstOrDefault(x => x.GameId == CurrentGame.Id);
            }
            else
            {
                return this.data.Inboxes.FirstOrDefault(x => x.Id == id);
            }

        }
        public void SellPlayerNews(int playerId, Game currentGame)
        {
            var team = this.data.VirtualTeams.FirstOrDefault(x => x.TeamId == currentGame.TeamId);
            var player = this.data.Players.FirstOrDefault(x => x.Id == playerId);
            var position = this.data.Positions.FirstOrDefault(x => x.Id == player.PositionId);

            var messageReview = $"{team.Name} sell player!";
            var fullMessage = $"{team.Name} sell {player.FirstName} {player.LastName}, a {player.Age} years old {position.Name}. {team.Name} will receive {player.Price} coins for the deal!";

            var inbox = new Inbox
            {
                Day = currentGame.Day,
                Year = currentGame.Year,
                Game = currentGame,
                GameId = currentGame.Id,
                MessageReview = messageReview,
                FullMessage = fullMessage,
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
        
    }
}
