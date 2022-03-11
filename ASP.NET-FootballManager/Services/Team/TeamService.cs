namespace ASP.NET_FootballManager.Services.Team
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.DataModels;
    public class TeamService : ITeamService
    {
        private readonly FootballManagerDbContext data;
        public TeamService(FootballManagerDbContext data)
        {
            this.data = data;   
        }

        public List<VirtualTeam> GenerateTeams(Game game)
        {
            var allTeam = this.data.Teams.ToList();

            var virtualTeams = allTeam.Select(x => new VirtualTeam
            {
                Team = x,
                TeamId = x.Id,
                Name = x.Name,
                Game = game,
                GameId = game.Id,
                LeagueId = x.LeagueId,
                IsPlayable = x.IsPlayable

            }).ToList();

            foreach (var team in virtualTeams)
            {
                this.data.VirtualTeams.Add(team);
            }
            this.data.SaveChanges();
            return virtualTeams;
        }
        public List<VirtualTeam> CurrentGameTeams(Game currentGame) => this.data.VirtualTeams.ToList();
        public void CalculateTeamOverall(List<VirtualTeam> teams)
        {
            teams.ForEach(team => team.Overall = 0);

            foreach (var team in teams)
            {
                var teamPlayers = this.data.Players.Where(x => x.TeamId == team.Id).ToList();
                var overallSum = teamPlayers.Sum(x => x.Overall);
                team.Overall = overallSum / teamPlayers.Count;
            }
            this.data.SaveChanges();
        }
    }
}
