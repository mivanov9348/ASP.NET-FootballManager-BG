namespace ASP.NET_FootballManager.Services.Game
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.Database.ImportDto;
    using ASP.NET_FootballManager.Data.DataModels;
    using Newtonsoft.Json;

    public class GameService : IGameService
    {
        private readonly FootballManagerDbContext data;
        private Random rnd;
        public GameService(FootballManagerDbContext data)
        {
            this.data = data;
            rnd = new Random();
        }
        public void CreateNewGame(Manager manager)
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
        }

        public void GenerateNames()
        {

            (string randomFn, string randomLn, City city, int age, Nation nation) = getPlayerInfo();

            var newPlayer = new Player
            {
                FirstName = randomFn,
                LastName = randomLn,
                City = city,
                CityId = city.Id,
                Age = age,
                Nation = nation,
                NationId = nation.Id,
                Matches = 0,
                Goals = 0,
                Saves = 0

            };
        }       
        
        private (string randomFn, string randomLn, City city, int age, Nation nation) getPlayerInfo()
        {
            var firstNamesPath = File.ReadAllText(@"C:\Users\mivan\source\repos\ASP.NET-FootballManager\ASP.NET-FootballManager\Data\Database\FirstNames.json");
            var lastNamesPath = File.ReadAllText(@"C:\Users\mivan\source\repos\ASP.NET-FootballManager\ASP.NET-FootballManager\Data\Database\LastNames.json");

            var firstNames = JsonConvert.DeserializeObject<FirstNamesDto[]>(firstNamesPath);
            var lastNames = JsonConvert.DeserializeObject<LastNamesDto[]>(lastNamesPath);
            var allCities = this.data.Cities.ToList();

            var randomFN = firstNames[rnd.Next(0, firstNames.Length)];

            var lastNamesByNation = lastNames.Where(x => x.NationId == randomFN.NationId).ToList();
            var randomLN = lastNamesByNation[rnd.Next(0, lastNamesByNation.Count)];

            var city = allCities[rnd.Next(0, allCities.Count())];

            var age = rnd.Next(17, 33);

            var nation = this.data.Nations.FirstOrDefault(x => x.Id == int.Parse(randomFN.NationId));

            return (randomFN.FirstName, randomLN.LastName, city, age, nation);

        }
    }
}
