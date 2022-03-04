namespace ASP.NET_FootballManager.Services.Common
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.DataModels;
    public class CommonService : ICommonService
    {
        private readonly FootballManagerDbContext data;
        public CommonService(FootballManagerDbContext data)
        {
            this.data = data;
        }

        public List<City> GetAllCities() => this.data.Cities.ToList();

        public List<Nation> GetAllNations() => this.data.Nations.ToList();

        public List<Player> GetAllPlayers() => this.data.Players.ToList();
        public List<Position> GetAllPositions() => this.data.Positions.ToList();
        public List<VirtualTeam> GetAllVirtualTeams() => this.data.VirtualTeams.ToList();
        public List<Game> GetAllUsersSaves(int managerId) => this.data.Games.Where(x => x.ManagerId == managerId).ToList();

        public List<Inbox> GetInboxMessages(int gameId) => this.data.Inboxes.Where(x => x.GameId == gameId).ToList();

        public List<Team> GetAllTeams() => this.data.Teams.ToList();

        public VirtualTeam GetCurrentTeam(Game currentGame) => this.data.VirtualTeams.FirstOrDefault(x => x.TeamId == currentGame.TeamId);

        public void DeleteNews(int id)
        {
            var currentInbox = this.data.Inboxes.FirstOrDefault(x => x.Id == id);
            this.data.Inboxes.Remove(currentInbox);
            this.data.SaveChanges();
        }

        public List<Fixture> GetFixture(int id)
        {
            if (id == 0)
            {
                return this.data.Fixtures.ToList();
            }
            else
            {
                return this.data.Fixtures.Where(x => x.LeagueId == id).ToList();
            }
        }

        public Team GetOriginalTeam(VirtualTeam currentVirtual) => this.data.Teams.FirstOrDefault(x => x.Id == currentVirtual.TeamId);
    }
}
