namespace ASP.NET_FootballManager.Services.Game
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;

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
        public Game GetCurrentGame(string userId) => this.data.Games.FirstOrDefault(x => x.UserId == userId);
        public Game CreateNewGame(Manager manager, string userId)
        {
            var currentTeam = this.data.Teams.FirstOrDefault(x => x.Id == manager.CurrentTeamId);
            var currentGameOptions = this.data.GameOptions.FirstOrDefault(x => x.UserId == manager.UserId);

            var newGame = new Game
            {
                Manager = manager,
                Season = 1,
                Year = 1,
                CupRound = 1,
                EuroCupRound = 1,
                LeagueRound = 1,
                Day = 1,
                ManagerId = manager.Id,
                Team = currentTeam,
                TeamId = currentTeam.Id,
                GameOption = currentGameOptions,
                GameOptionId = currentGameOptions.Id,
                UserId = userId
            };

            currentGameOptions.Games.Add(newGame);
            this.data.Games.Add(newGame);
            this.data.SaveChanges();
            return newGame;
        }
        public void NextDay(Game currentGame)
        {
            currentGame.Day += 1;
            this.data.SaveChanges();
        }
        public void ResetGame(Game CurrentGame)
        {
            CurrentGame.Day = 1;
            CurrentGame.Year += 1;
            CurrentGame.Season += 1;
            CurrentGame.CupRound = 1;
            CurrentGame.LeagueRound = 1;
            CurrentGame.EuroCupRound = 1;
            this.data.SaveChanges();
        }
        public void ResetSave(string UserId)
        {
            var userManager = this.data.Managers.FirstOrDefault(x => x.UserId == UserId);
            var userGame = this.data.Games.FirstOrDefault(x => x.ManagerId == userManager.Id);

            if (userGame != null)
            {
                this.data.Matches.RemoveRange(this.data.Matches.Where(c => c.GameId == userGame.Id));
                this.data.Players.RemoveRange(this.data.Players.Where(c => c.GameId == userGame.Id));
                this.data.Fixtures.RemoveRange(this.data.Fixtures.Where(c => c.GameId == userGame.Id));
                this.data.Inboxes.RemoveRange(this.data.Inboxes.Where(c => c.GameId == userGame.Id));
                this.data.VirtualTeams.RemoveRange(this.data.VirtualTeams.Where(c => c.GameId == userGame.Id));
                this.data.Days.RemoveRange(this.data.Days.Where(c => c.GameId == userGame.Id));
                this.data.Games.RemoveRange(this.data.Games.Where(x => x.Id == userGame.Id));
                this.data.Managers.RemoveRange(this.data.Managers.Where(x => x.Id == userManager.Id));
            }
            this.data.SaveChanges();
        }
    }
}
