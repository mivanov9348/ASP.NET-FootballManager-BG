namespace ASP.NET_FootballManager.Services.Game
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.Database.ImportDto;
    using ASP.NET_FootballManager.Data.DataModels;
    using ASP.NET_FootballManager.Services.League;
    using ASP.NET_FootballManager.Services.Team;
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

        public Game CreateNewGame(Manager manager)
        {
            var currentTeam = this.data.Teams.FirstOrDefault(x => x.Id == manager.CurrentTeamId);

            var newGame = new Game
            {
                Manager = manager,
                Season = 1,
                Year = 1,
                EuroCupRound = 1,
                LeagueRound = 1,
                Day = 1,
                ManagerId = manager.Id,
                Team = currentTeam,
                TeamId = currentTeam.Id
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


    }
}
