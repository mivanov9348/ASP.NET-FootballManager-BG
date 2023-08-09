namespace FootballManager.Core.Services.Player.PlayerGenerator
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.Constant;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Services.Attribute;
    using FootballManager.Core.Services.Player.PlayerData;
    using FootballManager.Core.Services.Player.PlayerStats;

    public class PlayerGeneratorService : IPlayerGeneratorService
    {
        private readonly FootballManagerDbContext data;
        private readonly IPlayerStatsService playerStats;
        private readonly IPlayerDataService playerDataService;
        private readonly IPlayerAttributeService playerAttribute;
        public PlayerGeneratorService(FootballManagerDbContext data, IPlayerStatsService playerStats, IPlayerAttributeService playerAttribute, IPlayerDataService playerDataService)
        {
            this.data = data;
            this.playerStats = playerStats;
            this.playerAttribute = playerAttribute;
            this.playerDataService = playerDataService;
        }
        public void GeneratePlayers(Game game, VirtualTeam team)
        {
            FillPlayersByPosition(DataConstants.StartingPlayersCount.gk, game, team, DataConstants.PositionOrder.Goalkeeper);
            FillPlayersByPosition(DataConstants.StartingPlayersCount.df, game, team, DataConstants.PositionOrder.Defender);
            FillPlayersByPosition(DataConstants.StartingPlayersCount.mf, game, team, DataConstants.PositionOrder.Midlefielder);
            FillPlayersByPosition(DataConstants.StartingPlayersCount.st, game, team, DataConstants.PositionOrder.Forward);
        }
        public void FillPlayersByPosition(int count, Game game, VirtualTeam team, int positionOrder)
        {
            for (int i = 0; i < count; i++)
            {
                var positionType = this.data.Positions.FirstOrDefault(x => x.Order == positionOrder);
                (string firstName, string lastName) = playerStats.getPlayerNames(team);
                (City city, int age, Nation nation) = playerStats.getPlayerInfo(team);

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
                playerStats.GetProfileImage(newPlayer);

                var playerAttributes = playerAttribute.CalculatePlayerAttributes(newPlayer);

                newPlayer.AttributesId = playerAttributes.Id;
                playerAttribute.CalculateOverall(newPlayer);

                this.data.SaveChanges();
            }
        }

        public void CreateFreeAgents(Game game, int gk, int df, int mf, int st)
        {
            var freeAgentsTeam = this.data.VirtualTeams.FirstOrDefault(x => x.IsPlayable == false && x.Name == "FreeAgents");
            playerDataService.RemovePlayers(freeAgentsTeam);

            for (int i = 0; i < gk; i++)
            {
                FillFreeAgents(1, freeAgentsTeam);
            }
            for (int i = 0; i < df; i++)
            {
                FillFreeAgents(2, freeAgentsTeam);
            }
            for (int i = 0; i < mf; i++)
            {
                FillFreeAgents(3, freeAgentsTeam);
            }
            for (int i = 0; i < st; i++)
            {
                FillFreeAgents(4, freeAgentsTeam);
            }
        }

        public void FillFreeAgents(int positionOrder, VirtualTeam team)
        {
            var positionType = this.data.Positions.FirstOrDefault(x => x.Order == positionOrder);
            (string FirstName, string LastName) = playerStats.getPlayerNames(team);
            (City city, int age, Nation nation) = playerStats.getPlayerInfo(team);
            var currentGame = this.data.Games.FirstOrDefault(x => x.Id == team.GameId);

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
                Team = team,
                TeamId = team.Id,
                Matches = 0,
                Goals = 0,
                Passes = 0,
                Game = currentGame,
                GameId = currentGame.Id,
                IsStarting11 = true,
                FreeAgent = true
            };
            this.data.Players.Add(newPlayer);
            this.data.SaveChanges();

            var playerAttributes = playerAttribute.CalculatePlayerAttributes(newPlayer);
            playerAttributes.PlayerId = newPlayer.Id;
            playerAttribute.CalculateOverall(newPlayer);

            playerStats.GetProfileImage(newPlayer);

            this.data.SaveChanges();
        }
    }
}
