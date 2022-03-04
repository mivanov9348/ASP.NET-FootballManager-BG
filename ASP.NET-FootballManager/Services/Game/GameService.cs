namespace ASP.NET_FootballManager.Services.Game
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.Database.ImportDto;
    using ASP.NET_FootballManager.Data.DataModels;
    using ASP.NET_FootballManager.Services.League;
    using Newtonsoft.Json;

    public class GameService : IGameService
    {
        private readonly FootballManagerDbContext data;
        private readonly ILeagueService leagueService;
        private Random rnd;
        public GameService(FootballManagerDbContext data, ILeagueService leagueService)
        {
            this.data = data;
            this.leagueService = leagueService;
            rnd = new Random();
        }
        public bool isExistGame(string UserId)
        {
            var currManager = this.data.Managers.FirstOrDefault(x => x.UserId == UserId);
            if (currManager != null)
            {
                return true;
            }
            return false;
        }
        public Game GetCurrentGame(int id) => this.data.Games.FirstOrDefault(x => x.ManagerId == id);
        public List<VirtualTeam> CurrentGameTeams(Game currentGame) => this.data.VirtualTeams.ToList();
        public void StartNewGame(Manager currentManager)
        {
            var currentGame = CreateNewGame(currentManager);
            var teams = GenerateTeams(currentGame);
            foreach (var team in teams)
            {
                GeneratePlayers(currentGame, team);
            }
            leagueService.GenerateFixtures(currentGame);               

        }
        private List<VirtualTeam> GenerateTeams(Game game)
        {
            var allTeam = this.data.Teams.ToList();

            var virtualTeams = allTeam.Select(x => new VirtualTeam
            {
                Team = x,
                TeamId = x.Id,
                Name = x.Name,
                Game = game,
                GameId = game.Id,
                LeagueId = x.LeagueId

            }).ToList();

            foreach (var team in virtualTeams)
            {
                this.data.VirtualTeams.Add(team);
            }
            this.data.SaveChanges();
            return virtualTeams;
        }
        public Game CreateNewGame(Manager manager)
        {
            var currentTeam = this.data.Teams.FirstOrDefault(x => x.Id == manager.CurrentTeamId);

            var newGame = new Game
            {
                Manager = manager,
                TeamId = currentTeam.Id,
                Team = currentTeam,
                Season = 1,
                Year = 1,
                Day = 1,
                ManagerId = manager.Id
            };

            this.data.Games.Add(newGame);
            this.data.SaveChanges();

            var newInbox = new Inbox
            {
                Game = newGame,
                GameId = newGame.Id,
                Message = $"{currentTeam.Name} appoint {manager.FirstName} {manager.LastName} as Manager!",
                Year = newGame.Year,
                Day = newGame.Day
            };
            this.data.Inboxes.Add(newInbox);
            this.data.SaveChanges();
            return newGame;
        }
        public void GeneratePlayers(Game game, VirtualTeam team)
        {
            (int goalkeepers, int defenders, int midfielders, int forwards) = GetCountForPosition();
            FillPlayersByPosition(goalkeepers, game, team, "Goalkeeper");
            FillPlayersByPosition(defenders, game, team, "Defender");
            FillPlayersByPosition(midfielders, game, team, "Midlefielder");
            FillPlayersByPosition(forwards, game, team, "Striker");

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
                    Saves = 0,
                    Game = game,
                    GameId = game.Id
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

    }
}
