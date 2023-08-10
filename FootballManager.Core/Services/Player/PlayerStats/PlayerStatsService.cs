namespace FootballManager.Core.Services.Player.PlayerStats
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.Constant;
    using ASP.NET_FootballManager.Data.Database.ImportDto;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Infrastructure.Data.DataModels;
    using Newtonsoft.Json;
    public class PlayerStatsService : IPlayerStatsService
    {
        private Random rnd;
        private readonly FootballManagerDbContext data;
        public PlayerStatsService(FootballManagerDbContext data)
        {
            this.rnd = new Random();
            this.data = data;
        }

        public (string firstName, string lastName) getPlayerNames(VirtualTeam team)
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
        public void GetProfileImage(Player player)
        {
            var path = "wwwroot/Images/Faces";
            int filesCount = Directory.GetFiles(path, "*", SearchOption.AllDirectories).Length;

            var randomFileNum = rnd.Next(1, filesCount);
            player.ProfileImage = $"{randomFileNum}.png";
            this.data.SaveChanges();
        }
        public (City city, int age, Nation nation) getPlayerInfo(VirtualTeam team)
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
        public void ResetPlayerStats(Game CurrentGame)
        {
            var allPlayers = this.data.Players.Where(x => x.GameId == CurrentGame.Id).ToList();

            foreach (var player in allPlayers)
            {
                var currentPlayerStats = this.data.PlayerStats.FirstOrDefault(x => x.PlayerId == player.Id);
                currentPlayerStats.Appearance = 0;
                currentPlayerStats.Goals = 0;
                currentPlayerStats.Passes = 0;
                currentPlayerStats.GoalsConceded = 0;
                currentPlayerStats.Tacklings = 0;
            }

            this.data.SaveChanges();
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
        public PlayerStats GetPlayerStatsByPlayer(Player player) => this.data.PlayerStats.FirstOrDefault(x => x.PlayerId == player.Id);
        public PlayerStats CreatePlayerStats(Player player)
        {
            var newPlayerStats = new PlayerStats
            {
                PlayerId = player.Id,
                Appearance = 0,
                Goals = 0,
                Passes = 0,
                GoalsConceded = 0,
                Tacklings = 0
            };
            this.data.PlayerStats.Add(newPlayerStats);
            this.data.SaveChanges();
            return newPlayerStats;
        }
    }
}
