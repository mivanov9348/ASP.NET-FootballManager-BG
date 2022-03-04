namespace ASP.NET_FootballManager.Services.Player
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.DataModels;
    using ASP.NET_FootballManager.Models;
    using System.Collections.Generic;

    public class PlayerService : IPlayerService
    {
        private readonly FootballManagerDbContext data;
        public PlayerService(FootballManagerDbContext data)
        {
            this.data = data;
        }

        public List<Player> GetPlayersByTeam(int teamId) => this.data.Players.Where(x => x.TeamId == teamId).ToList();
        public PlayersViewModel SortingPlayers(int sortBy)
        {
            var newModel = new PlayersViewModel
            {
                Nations = this.data.Nations.ToList(),
                Cities = this.data.Cities.ToList(),
                Players = this.data.Players.ToList(),
                Positions = this.data.Positions.ToList(),
                Teams = this.data.VirtualTeams.ToList(),

            };

            switch (sortBy)
            {
                case 1:
                    newModel.Players = this.data.Players.OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList();
                    return newModel;
                case 2:
                    newModel.Players = this.data.Players.OrderBy(x => x.Team.Name).ThenByDescending(x => x.FirstName).ToList();
                    return newModel;
                case 3:
                    newModel.Players = this.data.Players.OrderBy(x => x.City.Name).ThenByDescending(x => x.FirstName).ToList();
                    return newModel;
                case 4:
                    newModel.Players = this.data.Players.OrderByDescending(x => x.Goals).ThenByDescending(x => x.FirstName).ToList();
                    return newModel;
                case 5:
                    newModel.Players = this.data.Players.OrderByDescending(x => x.Saves).ThenByDescending(x => x.FirstName).ToList();
                    return newModel;
                case 6:
                    newModel.Players = this.data.Players.OrderByDescending(x => x.Attack).ThenByDescending(x => x.FirstName).ToList();
                    return newModel;
                case 7:
                    newModel.Players = this.data.Players.OrderByDescending(x => x.Defense).ThenByDescending(x => x.FirstName).ToList();
                    return newModel;
                case 8:
                    newModel.Players = this.data.Players.OrderByDescending(x => x.Overall).ThenByDescending(x => x.FirstName).ToList();
                    return newModel;
                default:
                    return newModel;                 
            }



        }
    }
}
