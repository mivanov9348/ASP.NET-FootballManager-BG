namespace FootballManager.Core.Services.Player.PlayerSorter
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Player;
    using FootballManager.Core.Models.Sorting;
    public class PlayerSorterService : IPlayerSorterService
    {
        private FootballManagerDbContext data;
        public PlayerSorterService(FootballManagerDbContext data)
        {
            this.data = data;
        }

        public PlayersViewModel SortingPlayers(PlayerStatsSorting sortBy, int id, Game currentGame)
        {
            var allPlayers = new List<Player>();

            switch (id)
            {
                case 0:
                    allPlayers = this.data.Players.Where(x => x.Team.IsPlayable == true && x.Team.LeagueId != null && x.GameId == currentGame.Id).ToList();
                    break;
                case 1:
                    allPlayers = this.data.Players.Where(x => x.Team.IsPlayable == true && x.Team.LeagueId != null && x.GameId == currentGame.Id).ToList();
                    break;
                case 2:
                    allPlayers = this.data.Players.Where(x => x.Team.IsPlayable == false && x.Team.Name != "FreeAgents" && x.GameId == currentGame.Id).ToList();
                    break;
            }

            var newModel = new PlayersViewModel
            {
                Nations = this.data.Nations.ToList(),
                Cities = this.data.Cities.ToList(),
                Players = allPlayers,
                Positions = this.data.Positions.ToList(),
                Teams = this.data.VirtualTeams.ToList(),
                AllPlayerAttributes = this.data.PlayerAttributes.ToList(),
                AllPlayerStats = this.data.PlayerStats.ToList(),
            };

            switch (sortBy)
            {
                case PlayerStatsSorting.FirstName:
                    newModel.Players = allPlayers.OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList();
                    break;
                case PlayerStatsSorting.Position:
                    newModel.Players = allPlayers.OrderBy(x => x.Team.Name).ThenByDescending(x => x.FirstName).ToList();
                    break;
                case PlayerStatsSorting.Age:
                    newModel.Players = allPlayers.OrderBy(x => x.Age).ThenByDescending(x => x.FirstName).ToList();
                    break;
                case PlayerStatsSorting.TeamName:
                    newModel.Players = allPlayers.OrderBy(x => x.Team.Name).ThenByDescending(x => x.FirstName).ToList();
                    break;
                case PlayerStatsSorting.CityName:
                    newModel.Players = allPlayers.OrderBy(x => x.City.Name).ThenByDescending(x => x.FirstName).ToList();
                    break;
                case PlayerStatsSorting.Appearance:
                    newModel.Players = allPlayers.OrderByDescending(x => x.PlayerStats.Appearance).ThenByDescending(x => x.FirstName).ToList();
                    break;
                case PlayerStatsSorting.Goals:
                    newModel.Players = allPlayers.OrderByDescending(x => x.PlayerStats.Goals).ThenByDescending(x => x.FirstName).ToList();
                    break;
                case PlayerStatsSorting.Passes:
                    newModel.Players = allPlayers.OrderByDescending(x => x.PlayerStats.Goals).ThenByDescending(x => x.FirstName).ToList();
                    break;
                case PlayerStatsSorting.Tacklings:
                    newModel.Players = allPlayers.OrderByDescending(x => x.PlayerStats.Tacklings).ThenByDescending(x => x.FirstName).ToList();
                    break;
                case PlayerStatsSorting.GoalsConceding:
                    newModel.Players = allPlayers.OrderByDescending(x => x.PlayerStats.GoalsConceded).ThenByDescending(x => x.FirstName).ToList();
                    break;
                case PlayerStatsSorting.Overall:
                    newModel.Players = allPlayers.OrderByDescending(x => x.Overall).ThenByDescending(x => x.FirstName).ToList();
                    break;
                default:
                    newModel.Players = allPlayers.OrderByDescending(x => x.Team.Name).ToList();
                    break;
            }

            return newModel;
        }
    }
}
