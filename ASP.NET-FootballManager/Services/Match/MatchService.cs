namespace ASP.NET_FootballManager.Services.Match
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.DataModels;
    using System.Collections.Generic;

    public class MatchService : IMatchService
    {
        private readonly FootballManagerDbContext data;
        public MatchService(FootballManagerDbContext data)
        {
            this.data = data;
        }
        public Fixture GetCurrentFixture(List<Fixture> dayFixtures, Game currentGame)
        {
            var currentTeam = this.data.VirtualTeams.FirstOrDefault(x => x.TeamId == currentGame.TeamId);

            return dayFixtures.FirstOrDefault(x => x.HomeTeamId == currentTeam.Id
                                                    || x.AwayTeamId == currentTeam.Id);

        }
        public List<Fixture> GetFixturesByDay(Game CurrentGame) => this.data.Fixtures.Where(x => x.Day == CurrentGame.Day).ToList();

        public List<Player> GetPcTeamPlayers(VirtualTeam managerTeam, Fixture currentFixture)
        {
            var teams = this.data.VirtualTeams.Where(x => x.Id == currentFixture.HomeTeamId || x.Id == currentFixture.AwayTeamId).ToList();
            var pcTeam = teams.FirstOrDefault(x => x.TeamId != managerTeam.Id);
            return this.data.Players.Where(x => x.TeamId == pcTeam.Id).ToList();
        }
        public List<Player> GetStarting11(int teamId) => this.data.Players.Where(x => x.TeamId == teamId && x.IsStarting11 == true).ToList();
        public List<Player> GetUserPlayers(VirtualTeam managerTeam)
        {
            var userTeam = this.data.VirtualTeams.FirstOrDefault(x => x.Id == managerTeam.Id);
            return this.data.Players.Where(x => x.TeamId == userTeam.Id).ToList();
        }
    }
}
