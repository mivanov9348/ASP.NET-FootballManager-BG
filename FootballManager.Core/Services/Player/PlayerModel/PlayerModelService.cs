namespace FootballManager.Core.Services.Player.PlayerModel
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Player;
    public class PlayerModelService : IPlayerModelService
    {
        private readonly FootballManagerDbContext data;
        public PlayerModelService(FootballManagerDbContext data)
        {
            this.data = data;
        }

        public PlayerDetailsViewModel PlayerDetailsViewModel(Player currentPlayer, Game currentGame)
        {
            var playerAttributes = this.data.PlayerAttributes.FirstOrDefault(x => x.PlayerId == currentPlayer.Id);
            var nations = this.data.Nations.ToList();
            var positions = this.data.Positions.ToList();
            var teams = this.data.VirtualTeams.Where(x => x.GameId == currentGame.Id).ToList();

            var playerDetailsViewModel = new PlayerDetailsViewModel
            {
                FullName = currentPlayer.FirstName + " " + currentPlayer.LastName,
                Age = currentPlayer.Age,
                City = currentPlayer.Team.Name,
                Position = positions.FirstOrDefault(x => x.Id == currentPlayer.PositionId).Name,
                ImageUrl = currentPlayer.ProfileImage,
                Goals = currentPlayer.Goals,
                Overall = currentPlayer.Overall,
                Nation = nations.FirstOrDefault(x => x.Id == currentPlayer.NationId).Name,
                Team = teams.FirstOrDefault(x => x.Id == currentPlayer.TeamId).Name,
                PlayerAttributes = this.data.PlayerAttributes.ToList(),
                OneOnOne = playerAttributes.OneOnOne,
                Reflexes = playerAttributes.Reflexes,
                Finishing = playerAttributes.Finishing,
                Passing = playerAttributes.Passing,
                Heading = playerAttributes.Heading,
                Tackling = playerAttributes.Tackling,
                Stamina = playerAttributes.Stamina,
                Strength = playerAttributes.Strength,
                Dribbling = playerAttributes.Dribbling,
                Positioning = playerAttributes.Positioning,
                BallControll = playerAttributes.BallControll,
                Pace = playerAttributes.Pace
            };
            return playerDetailsViewModel;
        }
    }
}
