namespace ASP.NET_FootballManager.Services.Team
{
    using ASP.NET_FootballManager.Data;
    using FootballManager.Infrastructure.Data.DataModels;

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

            var curentGameOptions = this.data.GameOptions.FirstOrDefault(x => x.Id == game.GameOptionId);

            var virtualTeams = allTeam.Select(x => new VirtualTeam
            {
                Team = x,
                TeamId = x.Id,
                Name = x.Name,
                Game = game,
                GameId = game.Id,
                LeagueId = x.LeagueId,
                IsPlayable = x.IsPlayable,
                ImageUrl = x.ImageUrl,
                CupId = x.CupId,
                EuropeanCupId = x.EuropeanCupId,
                Cup = x.Cup,
                EuropeanCup = x.EuropeanCup,
                IsEuroParticipant = x.IsEuroParticipant,
                IsCupParticipant = x.IsCupParticipant,
                Budget = curentGameOptions.StartingCoins

            }).ToList();

            foreach (var team in virtualTeams)
            {
                this.data.VirtualTeams.Add(team);
            }
            this.data.SaveChanges();
            return virtualTeams;
        }
        public List<VirtualTeam> CurrentGameTeams(Game currentGame) => this.data.VirtualTeams.ToList();
        public void CalculateTeamOverall(VirtualTeam team)
        {
            team.Overall = 0;
            var teamPlayers = this.data.Players.Where(x => x.TeamId == team.Id).ToList();
            var overallSum = teamPlayers.Sum(x => x.Overall);
            team.Overall = Convert.ToInt32(overallSum) / teamPlayers.Count;

            this.data.SaveChanges();
        }      
        public void ResetTeams(Game currentGame)
        {
            var allTeams = this.data.VirtualTeams.Where(x => x.GameId == currentGame.Id);

            foreach (var team in allTeams)
            {
                team.Matches = 0;
                team.Wins = 0;
                team.Draws = 0;
                team.Loses = 0;
                team.GoalScored = 0;
                team.GoalAgainst = 0;
                team.GoalDifference = 0;
                team.Points = 0;
            }
            this.data.SaveChanges();
        }
        public async Task<List<VirtualTeam>> GetAllVirtualTeams(Game currentGame) => await Task.Run(() => this.data.VirtualTeams.Where(x => x.GameId == currentGame.Id).ToList());
        public async Task<List<Team>> GetAllTeams() => await Task.Run(() => this.data.Teams.ToList());
        public async Task<VirtualTeam> GetCurrentTeam(Game currentGame) => await Task.Run(() => this.data.VirtualTeams.FirstOrDefault(x => x.TeamId == currentGame.TeamId));
        public async Task<Team> GetOriginalTeam(VirtualTeam currentVirtual, Game CurrentGame) => await Task.Run(() => this.data.Teams.FirstOrDefault(x => x.Id == currentVirtual.TeamId));
        public async Task<VirtualTeam> GetTeamById(int? teamId) => await Task.Run(() => this.data.VirtualTeams.FirstOrDefault(x => x.Id == teamId));
        public async Task<List<Team>> GetAllPlayableTeams() => await Task.Run(() => this.data.Teams.Where(x => x.IsPlayable == true).ToList());

        public Team GetManagerTeam(Manager currentManager) => this.data.Teams.FirstOrDefault(x => x.Id == currentManager.CurrentTeamId);
    }
}
