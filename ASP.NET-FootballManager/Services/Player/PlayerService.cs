namespace ASP.NET_FootballManager.Services.Player
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.Database.ImportDto;
    using ASP.NET_FootballManager.Data.DataModels;
    using ASP.NET_FootballManager.Models;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class PlayerService : IPlayerService
    {
        private readonly FootballManagerDbContext data;
        private Random rnd;
        public PlayerService(FootballManagerDbContext data)
        {
            this.rnd = new Random();
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
                    newModel.Players = this.data.Players.OrderByDescending(x => x.CleanSheets).ThenByDescending(x => x.FirstName).ToList();
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
        public void GeneratePlayers(Game game, VirtualTeam team)
        {
            (int goalkeepers, int defenders, int midfielders, int forwards) = GetCountForPosition();
            FillPlayersByPosition(goalkeepers, game, team, "Goalkeeper");
            FillPlayersByPosition(defenders, game, team, "Defender");
            FillPlayersByPosition(midfielders, game, team, "Midlefielder");
            FillPlayersByPosition(forwards, game, team, "Striker");
        }
        public void CreateFreeAgents(Game game, int gk, int df, int mf, int st)
        {
            var freeAgentsTeam = this.data.VirtualTeams.FirstOrDefault(x => x.IsPlayable == false);
            for (int i = 0; i < gk; i++)
            {
                FillFreeAgents("Goalkeeper");
            }
            for (int i = 0; i < df; i++)
            {
                FillFreeAgents("Defender");
            }
            for (int i = 0; i < mf; i++)
            {
                FillFreeAgents("Midlefielder");
            }
            for (int i = 0; i < st; i++)
            {
                FillFreeAgents("Striker");
            }

            void FillFreeAgents(string position)
            {
                var positionType = this.data.Positions.FirstOrDefault(x => x.Name == position);
                (string firstName, string lastName, City city, int age, Nation nation) = getPlayerInfo();
                (int defense, int attack, int speed, int overall) = CalculatePlayerAttributes(position);
                var newPlayer = new Player
                {
                    FirstName = firstName,
                    LastName = lastName,
                    City = city,
                    CityId = city.Id,
                    Age = age,
                    Nation = nation,
                    NationId = nation.Id,
                    Speed = speed,
                    Attack = attack,
                    Defense = defense,
                    Overall = overall,
                    Position = positionType,
                    PositionId = positionType.Id,
                    Team = freeAgentsTeam,
                    TeamId = freeAgentsTeam.Id,
                    Matches = 0,
                    Goals = 0,
                    CleanSheets = 0,
                    Game = game,
                    GameId = game.Id,
                    IsStarting11 = true
                };
                this.data.Players.Add(newPlayer);
                this.data.SaveChanges();
            }

        }
        public void CalculatingPlayersPrice()
        {
            var allTeams = this.data.Players.ToList();
            allTeams.ForEach(x => x.Price = x.Overall * 3);
            this.data.SaveChanges();

        }
        private (int goalkeepers, int defenders, int midfielders, int forwards) GetCountForPosition()
        {
            var goalkeepers = 1;
            var defenders = 4;
            var midfielders = 4;
            var forwards = 2;
            return (goalkeepers, defenders, midfielders, forwards);
        }
        private void FillPlayersByPosition(int count, Game game, VirtualTeam team, string position)
        {
            for (int i = 0; i < count; i++)
            {
                var positionType = this.data.Positions.FirstOrDefault(x => x.Name == position);
                (string firstName, string lastName, City city, int age, Nation nation) = getPlayerInfo();
                (int defense, int attack, int speed, int overall) = CalculatePlayerAttributes(position);
                var newPlayer = new Player
                {
                    FirstName = firstName,
                    LastName = lastName,
                    City = city,
                    CityId = city.Id,
                    Age = age,
                    Nation = nation,
                    NationId = nation.Id,
                    Speed = speed,
                    Attack = attack,
                    Defense = defense,
                    Overall = overall,
                    LeagueId = team.LeagueId,
                    Position = positionType,
                    PositionId = positionType.Id,
                    Team = team,
                    TeamId = team.Id,
                    Matches = 0,
                    Goals = 0,
                    CleanSheets = 0,
                    Game = game,
                    GameId = game.Id,
                    IsStarting11 = true
                };
                this.data.Players.Add(newPlayer);
                this.data.SaveChanges();
            }
        }
        private (int defense, int attack, int speed, int overall) CalculatePlayerAttributes(string position)
        {
            if (position == "Goalkeeper" || position == "Defender")
            {
                var defense = rnd.Next(50, 100);
                var attack = rnd.Next(20, 60);
                var speed = rnd.Next(20, 60);
                var overall = (defense + attack + speed) / 3;

                return (defense, attack, speed, overall);
            }
            if (position == "Midlefielder" || position == "Striker")
            {
                var defense = rnd.Next(20, 70);
                var attack = rnd.Next(40, 100);
                var speed = rnd.Next(30, 100);
                var overall = (defense + attack + speed) / 3;

                return (defense, attack, speed, overall);
            }
            return (20, 20, 20, 20);

        }
        private (string firstName, string lastName, City city, int age, Nation nation) getPlayerInfo()
        {

            var firstNamesPath = File.ReadAllText(@"C:\Users\mivan\source\repos\ASP.NET-FootballManager\ASP.NET-FootballManager\Data\Database\FirstNames.json");
            var lastNamesPath = File.ReadAllText(@"C:\Users\mivan\source\repos\ASP.NET-FootballManager\ASP.NET-FootballManager\Data\Database\LastNames.json");

            var firstNames = JsonConvert.DeserializeObject<FirstNamesDto[]>(firstNamesPath);
            var lastNames = JsonConvert.DeserializeObject<LastNamesDto[]>(lastNamesPath);
            var allCities = this.data.Cities.ToList();

            var randomFN = firstNames[rnd.Next(0, firstNames.Length)];

            var lastNamesByNation = lastNames.Where(x => x.NationId == randomFN.NationId).ToList();
            var randomLN = lastNamesByNation[rnd.Next(0, lastNamesByNation.Count)];

            var age = rnd.Next(17, 33);
            var nation = this.data.Nations.FirstOrDefault(x => x.Id == int.Parse(randomFN.NationId));
            var city = allCities.Where(x => x.NationId == nation.Id).ToList()[rnd.Next(0, allCities.Count())];

            return (randomFN.FirstName, randomLN.LastName, city, age, nation);

        }
        public List<Player> GetStartingEleven(int teamId) => this.data.Players.Where(x => x.IsStarting11 == true && x.TeamId == teamId).ToList();
        public List<Player> GetSubstitutes(int teamId) => this.data.Players.Where(x => x.IsStarting11 == false && x.TeamId == teamId).ToList();
        public void Substitution(int playerId, string action)
        {
            var currentPlayer = this.data.Players.FirstOrDefault(x => x.Id == playerId);
            switch (action)
            {
                case "Add":
                    currentPlayer.IsStarting11 = true;
                    break;
                case "Remove":
                    currentPlayer.IsStarting11 = false;
                    break;
                default:
                    break;
            }
            this.data.SaveChanges();
        }
        public Player GetPlayer(VirtualTeam team)
        {
            var players = this.data.Players.Where(x => x.GameId == team.GameId && x.IsStarting11 == true && x.TeamId == team.Id).ToList();
            return players[rnd.Next(0, players.Count)];
        }
    }
}
