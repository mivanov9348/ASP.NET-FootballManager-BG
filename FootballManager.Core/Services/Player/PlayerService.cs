namespace ASP.NET_FootballManager.Services.Player
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.Constant;
    using ASP.NET_FootballManager.Data.Database.ImportDto;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Player;
    using FootballManager.Core.Models.Sorting;
    using FootballManager.Core.Services.Attribute;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Reflection.Metadata;

    public class PlayerService : IPlayerService
    {
        private readonly FootballManagerDbContext data;
        private readonly IPlayerAttributeService attributeService;
        private Random rnd;
        public PlayerService(IPlayerAttributeService attributeService, FootballManagerDbContext data)
        {
            this.rnd = new Random();
            this.data = data;
            this.attributeService = attributeService;
        }
        public void GeneratePlayers(Game game, VirtualTeam team)
        {
            FillPlayersByPosition(DataConstants.StartingPlayersCount.gk, game, team, DataConstants.PositionOrder.Goalkeeper);
            FillPlayersByPosition(DataConstants.StartingPlayersCount.df, game, team, DataConstants.PositionOrder.Defender);
            FillPlayersByPosition(DataConstants.StartingPlayersCount.mf, game, team, DataConstants.PositionOrder.Midlefielder);
            FillPlayersByPosition(DataConstants.StartingPlayersCount.st, game, team, DataConstants.PositionOrder.Forward);
        }
        public void CreateFreeAgents(Game game, int gk, int df, int mf, int st)
        {
            var freeAgentsTeam = this.data.VirtualTeams.FirstOrDefault(x => x.IsPlayable == false && x.Name == "FreeAgents");
            RemovePlayers(freeAgentsTeam);
            for (int i = 0; i < gk; i++)
            {
                FillFreeAgents(1);
            }
            for (int i = 0; i < df; i++)
            {
                FillFreeAgents(2);
            }
            for (int i = 0; i < mf; i++)
            {
                FillFreeAgents(3);
            }
            for (int i = 0; i < st; i++)
            {
                FillFreeAgents(4);
            }

            void FillFreeAgents(int positionOrder)
            {
                var positionType = this.data.Positions.FirstOrDefault(x => x.Order == positionOrder);
                (string FirstName, string LastName) = getPlayerNames(freeAgentsTeam);
                (City city, int age, Nation nation) = getPlayerInfo(freeAgentsTeam);

                var newPlayer = new Player
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    City = city,
                    CityId = city.Id,
                    Age = age,
                    Nation = nation,
                    NationId = nation.Id,
                    Position = positionType,
                    PositionId = positionType.Id,
                    Team = freeAgentsTeam,
                    TeamId = freeAgentsTeam.Id,
                    Matches = 0,
                    Goals = 0,
                    Passes = 0,
                    Game = game,
                    GameId = game.Id,
                    IsStarting11 = true,
                    FreeAgent = true
                };
                this.data.Players.Add(newPlayer);
                this.data.SaveChanges();

                var playerAttributes = attributeService.CalculatePlayerAttributes(newPlayer);
                playerAttributes.PlayerId = newPlayer.Id;
                attributeService.CalculateOverall(newPlayer);

                GetProfileImage(newPlayer);

                this.data.SaveChanges();
            }

        }
        public async Task<Player> GetRandomPlayer(VirtualTeam team)
        {
            var players = await Task.Run(() => this.data.Players.Where(x => x.GameId == team.GameId && x.IsStarting11 == true && x.TeamId == team.Id && x.Position.Name != "Goalkeeper").ToList());
            return players[rnd.Next(0, players.Count)];
        }
        public async Task<List<Player>> GetPlayersByTeam(int teamId) => await Task.Run(() => this.data.Players.Where(x => x.TeamId == teamId).ToList());
        public async Task<Player> GetPlayerById(int id) => await Task.Run(() => this.data.Players.FirstOrDefault(x => x.Id == id));
        public async Task<Player> GetLeagueGoalscorer(Game CurrentGame, int leagueId)
        {
            if (leagueId == 0)
            {
                leagueId = await Task.Run(() => this.data.Leagues.FirstOrDefault(x => x.Level == DataConstants.LeagueLevels.FirstLevel).Id);
            }

            var currentComp = this.data.Leagues.FirstOrDefault(x => x.Id == leagueId);
            return await Task.Run(() => this.data.Players.OrderByDescending(x => x.Goals).ThenByDescending(x => x.Matches).FirstOrDefault(x => x.GameId == CurrentGame.Id && x.LeagueId == leagueId));
        }
        public async Task<List<Player>> GetStartingEleven(int teamId) => await Task.Run(() => this.data.Players.Where(x => x.IsStarting11 == true && x.TeamId == teamId).ToList());
        public async Task<List<Player>> GetSubstitutes(int teamId) => await Task.Run(() => this.data.Players.Where(x => x.IsStarting11 == false && x.TeamId == teamId).ToList());
        public void RemovePlayers(VirtualTeam freeAgentsTeam)
        {
            var allPlayers = this.data.Players.Where(x => x.TeamId == freeAgentsTeam.Id).ToList();

            foreach (var item in allPlayers)
            {
                this.data.Players.Remove(item);
            }
            this.data.SaveChanges();
        }
        public PlayersViewModel SortingPlayers(PlayerSorting sortBy, int id, Game currentGame)
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
                AllPlayerAttributes = this.data.PlayerAttributes.ToList()
            };

            switch (sortBy)
            {
                case PlayerSorting.FirstName:
                    newModel.Players = allPlayers.OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList();
                    break;
                case PlayerSorting.TeamName:
                    newModel.Players = allPlayers.OrderBy(x => x.Team.Name).ThenByDescending(x => x.FirstName).ToList();
                    break;
                case PlayerSorting.CityName:
                    newModel.Players = allPlayers.OrderBy(x => x.City.Name).ThenByDescending(x => x.FirstName).ToList();
                    break;
                case PlayerSorting.Goals:
                    newModel.Players = allPlayers.OrderByDescending(x => x.Goals).ThenByDescending(x => x.FirstName).ToList();
                    break;
                case PlayerSorting.Passes:
                    newModel.Players = allPlayers.OrderByDescending(x => x.Passes).ThenByDescending(x => x.FirstName).ToList();
                    break;
                case PlayerSorting.Overall:
                    newModel.Players = allPlayers.OrderByDescending(x => x.Overall).ThenByDescending(x => x.FirstName).ToList();
                    break;
                case PlayerSorting.Price:
                    newModel.Players = allPlayers.OrderByDescending(x => x.Price).ThenByDescending(x => x.FirstName).ToList();
                    break;
                default:
                    newModel.Players = allPlayers.OrderByDescending(x => x.Team.Name).ToList();
                    break;
            }

            return newModel;
        }
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
        public void CalculatingPlayersPrice(Game CurrentGame)
        {

            var allPlayers = this.data.Players.Where(x => x.GameId == CurrentGame.Id).ToList();

            foreach (var player in allPlayers)
            {

                double currAgeFactor = ((Convert.ToDouble(DataConstants.Age.maxAge) - Convert.ToDouble(player.Age)) / (Convert.ToDouble(DataConstants.Age.maxAge) - Convert.ToDouble(DataConstants.Age.minAge)));
                var result = currAgeFactor * player.Overall;
                player.Price = Math.Round(result, 2, MidpointRounding.ToEven);
            }

            this.data.SaveChanges();
        }
        public void ResetPlayerStats(Game CurrentGame)
        {
            var allPlayers = this.data.Players.Where(x => x.GameId == CurrentGame.Id).ToList();

            foreach (var player in allPlayers)
            {
                player.Goals = 0;
                player.Passes = 0;
            }

            this.data.SaveChanges();
        }
        private (City city, int age, Nation nation) getPlayerInfo(VirtualTeam team)
        {
            var currentTeam = this.data.Teams.FirstOrDefault(x => x.Id == team.TeamId);
            var currentNation = this.data.Nations.FirstOrDefault(x => x.Id == currentTeam.NationId);
            var currentGame = this.data.Games.FirstOrDefault(x => x.Id == team.GameId);
            var currentOptions = this.data.GameOptions.FirstOrDefault(x => x.Id == currentGame.GameOptionId);

            if (currentNation == null)
            {
                var allNations = this.data.Nations.ToList();
                currentNation = allNations[rnd.Next(0, allNations.Count())];
            }

            var age = rnd.Next(currentOptions.PlayerMinimumAge, currentOptions.PlayerMaximumAge);
            var nation = this.data.Nations.FirstOrDefault(x => x.Id == currentNation.Id);

            var allCities = this.data.Cities.Where(x => x.NationId == nation.Id).ToList();

            if (allCities.Count < 1)
            {
                allCities = this.data.Cities.Where(x => x.Nation.Name == "Bulgaria").ToList();
            }

            var city = allCities[rnd.Next(0, allCities.Count())];

            return (city, age, nation);

        }
        private void GetProfileImage(Player player)
        {
            var path = "wwwroot/Images/Faces";
            int filesCount = Directory.GetFiles(path, "*", SearchOption.AllDirectories).Length;

            var randomFileNum = rnd.Next(1, filesCount);
            player.ProfileImage = $"{randomFileNum}.png";
            this.data.SaveChanges();
        }
        private (string firstName, string lastName) getPlayerNames(VirtualTeam team)
        {
            var firstName = "";
            var lastName = "";

            var currentTeam = this.data.Teams.FirstOrDefault(x => x.Id == team.TeamId);
            var currentNation = this.data.Nations.FirstOrDefault(x => x.Id == currentTeam.NationId);

            if (currentNation == null)
            {
                var allNations = this.data.Nations.ToList();
                currentNation = allNations[rnd.Next(0, allNations.Count())];
            }

            var currentDirectory = Environment.CurrentDirectory;
            currentDirectory = currentDirectory.Remove(currentDirectory.IndexOf(@"\ASP.NET-FootballManager"));

            var firstNamesPath = File.ReadAllText(@$"{currentDirectory}/ASP.NET-FootballManager-BG\FootballManager.Infrastructure\Seeding\NamesData\FirstNames.json");
            var lastNamesPath = File.ReadAllText(@$"{currentDirectory}/ASP.NET-FootballManager-BG\FootballManager.Infrastructure\Seeding\NamesData\LastNames.json");

            var firstNames = JsonConvert.DeserializeObject<FirstNamesDto[]>(firstNamesPath);
            var lastNames = JsonConvert.DeserializeObject<LastNamesDto[]>(lastNamesPath);

            var firstNamesByNation = firstNames.Where(x => x.NationName == currentNation.Name).ToList();
            var lastNamesByNation = lastNames.Where(x => x.NationName == currentNation.Name).ToList();

            if (firstNamesByNation.Count > 1 && lastNamesByNation.Count > 1)
            {
                firstName = firstNamesByNation[rnd.Next(0, firstNamesByNation.Count)].FirstName;
                lastName = lastNamesByNation[rnd.Next(0, lastNamesByNation.Count)].LastName;
            }
            else
            {
                firstName = firstNames[rnd.Next(0, firstNames.Length)].FirstName;
                lastName = lastNames[rnd.Next(0, lastNames.Length)].LastName;
            }

            return (firstName, lastName);
        }
        private void FillPlayersByPosition(int count, Game game, VirtualTeam team, int positionOrder)
        {
            for (int i = 0; i < count; i++)
            {
                var positionType = this.data.Positions.FirstOrDefault(x => x.Order == positionOrder);
                (string firstName, string lastName) = getPlayerNames(team);
                (City city, int age, Nation nation) = getPlayerInfo(team);

                var newPlayer = new Player
                {
                    FirstName = firstName,
                    LastName = lastName,
                    City = city,
                    CityId = city.Id,
                    Age = age,
                    Nation = nation,
                    NationId = nation.Id,
                    LeagueId = team.LeagueId,
                    Position = positionType,
                    PositionId = positionType.Id,
                    Team = team,
                    TeamId = team.Id,
                    Matches = 0,
                    Goals = 0,
                    Passes = 0,
                    Game = game,
                    GameId = game.Id,
                    IsStarting11 = true,
                    FreeAgent = false
                };
                this.data.Players.Add(newPlayer);
                GetProfileImage(newPlayer);

                var playerAttributes = attributeService.CalculatePlayerAttributes(newPlayer);

                newPlayer.AttributesId = playerAttributes.Id;
                attributeService.CalculateOverall(newPlayer);

                this.data.SaveChanges();
            }
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
