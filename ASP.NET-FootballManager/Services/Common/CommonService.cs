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
        public List<VirtualTeam> GetAllVirtualTeams(Game currentGame) => this.data.VirtualTeams.Where(x => x.GameId == currentGame.Id).ToList();
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

        public List<Fixture> GetFixture(int id, int round)
        {
            if (id == 0 || round == 0)
            {
                return this.data.Fixtures.Where(x => x.Round == 1 && x.League.Level == 1).ToList();
            }
            else
            {
                return this.data.Fixtures.Where(x => x.LeagueId == id && x.Round == round).ToList();
            }
        }

        public Team GetOriginalTeam(VirtualTeam currentVirtual) => this.data.Teams.FirstOrDefault(x => x.Id == currentVirtual.TeamId);

        public VirtualTeam GetTeamById(int teamId) => this.data.VirtualTeams.FirstOrDefault(x => x.Id == teamId);

        public int GetAllRounds(int leagueId)
        {
            var allFixt = new List<Fixture>();
            if (leagueId == 0)
            {
                allFixt = this.data.Fixtures.Where(x => x.League.Level == 1).ToList();
            }
            else
            {
                allFixt = this.data.Fixtures.Where(x => x.LeagueId == leagueId).ToList();
            }
            allFixt.Reverse();
            return allFixt.First().Round;
        }
    }
}
